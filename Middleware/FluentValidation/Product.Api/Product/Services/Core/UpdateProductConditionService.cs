using AutoMapper;
using Domain.Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.Domain;
using TXC.Common.MessageContract.Product;
using TXC.Proto.Product;

namespace Services.Core
{
    public class UpdateProductConditionService : IUpdateProductConditionService
    {
        private IDbConnection _dbConnection;

        public UpdateProductConditionService()
        {

        }

        public async Task<ProductUpdatingConditionResponse> UpdateProductCondition(
            ProductUpdatingConditionRequest request
            , CheckProductDel _checkProductDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , UpdateProductConditionDel _updateProductConditionDel
            , SendProductConditionMessageDel _sendProductConditionMessageDel
            , CompareConditionIsTheSameDel _compareConditionIsTheSameDel
            , GetProductConditionDel _getProductConditionDel
            , CheckReverseLimitNameDel _checkReverseLimitNameDel
            , ConvertProductConditionResponseToProductConditionDel _convertProductConditionResponseToProductConditionDel
            , ConvertProductUpdatingConditionRequestToProductConditionDel _convertProductUpdatingConditionRequestToProductConditionDel
            , ConvertProductUpdatingConditionRequestToUpdateProductConditionV1Del _convertProductUpdatingConditionRequestToUpdateProductConditionV1Del
            )
        {
            ProductUpdatingConditionResponse response = new ProductUpdatingConditionResponse();

            try
            {
                if (string.IsNullOrEmpty(request.TenantName))
                    return new ProductUpdatingConditionResponse() { Success = false, Message = "TenantName header required" };

                if (request.ProductId <= 0)
                    return new ProductUpdatingConditionResponse() { Success = false, Message = "Invalid request" };

                //check tx2 connector config
                TenantConfig queueNameConfig = await getConfig("TX2ConnectorQueueName", request.TenantId);

                // initialize db connection
                Response<IDbConnection> conn = await getDBConnection(request.TenantId);

                if (!conn.Success)
                    return new ProductUpdatingConditionResponse() { Success = false, Message = "Error in Tenant DB" };

                _dbConnection = conn.Data;

                Tuple<int, int> productChecker = await _checkProductDel(request.ProductId, _dbConnection);
                if (productChecker.Item1 == -1)
                {
                    return new ProductUpdatingConditionResponse() { Success = false, Message = "Product not found" };
                }

                bool _reverseLimitExist = await _checkReverseLimitNameDel(request.ReverseLimitId, _dbConnection);
                if (!_reverseLimitExist)
                {
                    return new ProductUpdatingConditionResponse() { Success = false, Message = "Invalid Reverse Limit Id" };
                }

                ProductConditionResponse originCondition = await _getProductConditionDel(request.ProductId, _dbConnection);
                ProductCondition originProductCondition = _convertProductConditionResponseToProductConditionDel(originCondition);
                ProductCondition requestProductCondition = _convertProductUpdatingConditionRequestToProductConditionDel(request);

                bool _twoConditionIsTheSame = _compareConditionIsTheSameDel(originProductCondition, requestProductCondition);
                if (_twoConditionIsTheSame)
                {
                    return new ProductUpdatingConditionResponse() { Success = false, Message = "No Change" };
                }

                int result = await _updateProductConditionDel(request, _dbConnection);

                if (result != 1)
                {
                    return new ProductUpdatingConditionResponse
                    {
                        Success = false,
                        Message = "Fail",
                        Data = request.ProductId
                    };
                }

                UpdateProductConditionV1 message = _convertProductUpdatingConditionRequestToUpdateProductConditionV1Del(request);
                message.TX2UserName = request.TX2UserName;
                message.TenantId = request.TenantId;

                bool sendMessageResult = await _sendProductConditionMessageDel(request.TenantId, queueNameConfig.Value, message);
                if (!sendMessageResult)
                {
                    return new ProductUpdatingConditionResponse
                    {
                        Success = false,
                        Message = "Fail to send to service bus."
                    };
                }

                return new ProductUpdatingConditionResponse
                {
                    Success = true,
                    Message = "Success",
                    Data = request.ProductId
                };
            }
            catch (Exception ex)
            {
                return new ProductUpdatingConditionResponse() { Success = false, Message = ex.Message };
            }
        }
    }
}
