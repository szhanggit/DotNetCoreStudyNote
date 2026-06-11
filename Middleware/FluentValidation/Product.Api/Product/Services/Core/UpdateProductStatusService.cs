using Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.CacheManagement;
using TXC.Common.Data;
using TXC.Common.Domain;
using TXC.Common.Data.TenantDbConnection;
using TXC.Common.MessageContract;
using TXC.Proto.Product;
using Dapper;
using TXC.Common.MessageContract.Product;

namespace Services.Core
{
    public class UpdateProductStatusService : IUpdateProductStatusService
    {
        private IDbConnection _dbConnection;

        public UpdateProductStatusService()
        {
        }

        public async Task<UpdateProductStatusResponse> UpdateProductStatus(
            UpdateProductStatusRequest request
            , SendProductUpdateStatusMessageDel _sendProductUpdateStatusMessageDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , UpdateProductStatusDel updateProductStatus
            , CheckProductDel _checkProductDel
            , CheckContractDel _checkContract)
        {
            try
            {
                if (string.IsNullOrEmpty(request.TenantName))
                    return new UpdateProductStatusResponse() { Success = false, Message = "TenantName header required" };

                if (request.ProductId <= 0)
                    return new UpdateProductStatusResponse() { Success = false, Message = "Invalid request" };

                if (request.Status != 0 && request.Status != 1)
                    return new UpdateProductStatusResponse() { Success = false, Message = "Invalid request" };

                //check tx2 connector config
                TenantConfig queueNameConfig = await getConfig("TX2ConnectorQueueName", request.TenantId);

                // initialize db connection
                Response<IDbConnection> conn = await getDBConnection(request.TenantId);

                if (!conn.Success)
                    return new UpdateProductStatusResponse() { Success = false, Message = "Error in Tenant DB" };

                _dbConnection = conn.Data;

                Tuple<int,int> productChecker = await _checkProductDel(request.ProductId, _dbConnection);
                if (productChecker.Item1 == -1)
                {
                    return new UpdateProductStatusResponse() { Success = false, Message = "Product not found" };
                }

                bool ifContractExists = await _checkContract(request.ProductId, _dbConnection);
                if (!ifContractExists)
                {
                    return new UpdateProductStatusResponse() { Success = false, Message = "Contract Required" };
                }

                int _result = await updateProductStatus(request, _dbConnection);

                if (_result == 0)
                {
                    return new UpdateProductStatusResponse
                    {
                        Success = false,
                        Message = "Nothing will be changed",
                        Data = request.ProductId
                    };
                }

                if (_result != 1)
                {
                    return new UpdateProductStatusResponse
                    {
                        Success = false,
                        Message = "Fail",
                        Data = request.ProductId
                    };
                }

                var message = new UpdateProductStatusV1()
                {
                    TX2UserName = request.TX2UserName,
                    ProductId = request.ProductId,
                    Status = request.Status,
                    TenantId = request.TenantId
                };
                bool sendMessageResult = await _sendProductUpdateStatusMessageDel(request.TenantId, queueNameConfig.Value, message);
                if (!sendMessageResult)
                {
                    return new UpdateProductStatusResponse
                    {
                        Success = false,
                        Message = "Fail to send to service bus."
                    };
                }

                return new UpdateProductStatusResponse
                {
                    Success = true,
                    Message = "Success",
                    Data = request.ProductId
                };
            }
            catch (Exception ex) 
            {
                return new UpdateProductStatusResponse() { Success = false, Message = ex.Message };
            }
        }
    }
}
