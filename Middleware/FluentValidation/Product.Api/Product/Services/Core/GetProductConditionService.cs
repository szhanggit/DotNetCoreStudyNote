using Google.Protobuf.WellKnownTypes;
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
    public class GetProductConditionService : IGetProductConditionService
    {
        private IDbConnection _dbConnection;

        public GetProductConditionService()
        {

        }

        public async Task<ProtoBaseResponse> GetProductCondition(
            ProductConditionRequest request
            , CheckProductDel _checkProductDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , GetProductConditionDel _getProductConditionDel
            )
        {            
            try
            {
                if (string.IsNullOrEmpty(request.TenantName))
                    return new ProtoBaseResponse() { Success = false, Message = "TenantName header required" };

                if (request.ProductId <= 0)
                    return new ProtoBaseResponse() { Success = false, Message = "Invalid request" };

                //check tx2 connector config
                TenantConfig queueNameConfig = await getConfig("TX2ConnectorQueueName", request.TenantId);

                // initialize db connection
                Response<IDbConnection> conn = await getDBConnection(request.TenantId);

                if (!conn.Success)
                    return new ProtoBaseResponse() { Success = false, Message = "Error in Tenant DB" };

                _dbConnection = conn.Data;

                Tuple<int, int> productChecker = await _checkProductDel(request.ProductId, _dbConnection);
                if (productChecker.Item1 == -1)
                {
                    return new ProtoBaseResponse() { Success = false, Message = "Product does not exist" };
                }

                ProductConditionResponse response = new ProductConditionResponse();
                response = await _getProductConditionDel(request.ProductId, _dbConnection);
                return new ProtoBaseResponse() { Success = true, Message = "Success", Data = Any.Pack(response) };
            }
            catch (Exception ex)
            {
                return new ProtoBaseResponse() { Success = false, Message = ex.Message };
            }
        }
    }
}
