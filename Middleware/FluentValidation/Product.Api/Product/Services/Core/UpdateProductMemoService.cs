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
    public class UpdateProductMemoService : IUpdateProductMemoService
    {
        private IDbConnection _dbConnection;

        public UpdateProductMemoService()
        {

        }

        public async Task<UpdateProductMemoResponse> UpdateProductBrand(
            UpdateProductMemoRequest request
            , CheckProductDel _checkProductDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , UpdateProductMemoDel _updateProductMemoDel
            , SendProductUpdateMemoMessageDel _sendProductUpdateMemoMessageDel
            )
        {
            try
            {
                if (string.IsNullOrEmpty(request.TenantName))
                    return new UpdateProductMemoResponse() { Success = false, Message = "TenantName header required" };

                if (request.ProductId <= 0)
                    return new UpdateProductMemoResponse() { Success = false, Message = "Invalid request" };

                //check tx2 connector config
                TenantConfig queueNameConfig = await getConfig("TX2ConnectorQueueName", request.TenantId);

                // initialize db connection
                Response<IDbConnection> conn = await getDBConnection(request.TenantId);

                if (!conn.Success)
                    return new UpdateProductMemoResponse() { Success = false, Message = "Error in Tenant DB" };

                _dbConnection = conn.Data;

                Tuple<int, int> productChecker = await _checkProductDel(request.ProductId, _dbConnection);
                if (productChecker.Item1 == -1)
                {
                    return new UpdateProductMemoResponse() { Success = false, Message = "Product not found" };
                }

                int result = await _updateProductMemoDel(request, _dbConnection);

                if (result != 1)
                {
                    return new UpdateProductMemoResponse
                    {
                        Success = false,
                        Message = "Fail",
                        Data = request.ProductId
                    };
                }

                var message = new UpdateProductMemoV1()
                {
                    TX2UserName = request.TX2UserName,
                    ProductId = request.ProductId,
                    TenantId = request.TenantId,
                    SalesNote = request.SalesNote,
                    CustomerServiceNote = request.CustomerServiceNote,
                    OperationNote = request.OperationNote
                };

                bool sendMessageResult = await _sendProductUpdateMemoMessageDel(request.TenantId, queueNameConfig.Value, message);
                if (!sendMessageResult)
                {
                    return new UpdateProductMemoResponse
                    {
                        Success = false,
                        Message = "Fail to send to service bus."
                    };
                }

                return new UpdateProductMemoResponse
                {
                    Success = true,
                    Message = "Success",
                    Data = request.ProductId
                };
            }
            catch (Exception ex)
            {
                return new UpdateProductMemoResponse() { Success = false, Message = ex.Message };
            }
        }
    }
}
