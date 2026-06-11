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
using Services.Interface;
using Dapper;
using TXC.Common.MessageContract.Product;

namespace Services.Core
{
    public class UpdateProductBrandService : IUpdateProductBrandService
    {
        private IDbConnection _dbConnection;

        public UpdateProductBrandService()
        {
        }

        public async Task<UpdateProductBrandResponse> UpdateProductBrand(
            UpdateProductBrandRequest request
            , CheckBrandDel checkBrandDel
            , SendProductUpdateBrandMessageDel _sendProductUpdateBrandMessageDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , UpdateProductBrandDel updateProductBrand
            , CheckProductDel _checkProductDel)
        {
            try
            {
                if (string.IsNullOrEmpty(request.TenantName))
                    return new UpdateProductBrandResponse() { Success = false, Message = "TenantName header required" };

                if (request.ProductId <= 0)
                    return new UpdateProductBrandResponse() { Success = false, Message = "Invalid request" };

                if (request.BrandId <= 0 && request.BrandId.HasValue)
                    return new UpdateProductBrandResponse() { Success = false, Message = "Invalid request" };

                //check tx2 connector config
                TenantConfig queueNameConfig = await getConfig("TX2ConnectorQueueName", request.TenantId);

                // initialize db connection
                Response<IDbConnection> conn = await getDBConnection(request.TenantId);

                if (!conn.Success)
                    return new UpdateProductBrandResponse() { Success = false, Message = "Error in Tenant DB" };

                _dbConnection = conn.Data;

                Tuple<int, int> productChecker = await _checkProductDel(request.ProductId, _dbConnection);
                if (productChecker.Item1 == -1)
                {
                    return new UpdateProductBrandResponse() { Success = false, Message = "Product does not exist" };
                }

                if (productChecker.Item2 != (int)MultipleSelectionType.General && productChecker.Item2 != (int)MultipleSelectionType.Child)
                {
                    return new UpdateProductBrandResponse() { Success = false, Message = "Product type invalid" };
                }

                Tuple<int, int> brandChecker = await checkBrandDel(request.BrandId, _dbConnection);
                if (brandChecker.Item1 == -1)
                {
                    return new UpdateProductBrandResponse() { Success = false, Message = "Brand does not exist" };
                }

                if (brandChecker.Item2 != 1)
                {
                    return new UpdateProductBrandResponse() { Success = false, Message = "Brand is not available" };
                }

                int _result = await updateProductBrand(request, _dbConnection);

                if (_result == 0)
                {
                    return new UpdateProductBrandResponse
                    {
                        Success = false,
                        Message = "Nothing will be changed"
                    };
                }

                if (_result != 1)
                {
                    return new UpdateProductBrandResponse
                    {
                        Success = false,
                        Message = "Fail"
                    };
                }

                var message = new UpdateProductBrandV1()
                {
                    TX2UserName = request.TX2UserName,
                    ProductId = request.ProductId,
                    BrandId = request.BrandId,
                    TenantId = request.TenantId
                };
                bool sendMessageResult = await _sendProductUpdateBrandMessageDel(request.TenantId, queueNameConfig.Value, message);
                if (!sendMessageResult)
                {
                    return new UpdateProductBrandResponse
                    {
                        Success = false,
                        Message = "Fail to send to service bus."
                    };
                }

                return new UpdateProductBrandResponse
                {
                    Success = true,
                    Message = "Success",
                    Data = request.ProductId
                };
            }
            catch (Exception ex)
            {
                return new UpdateProductBrandResponse() { Success = false, Message = ex.Message };
            }
        }
    }
}
