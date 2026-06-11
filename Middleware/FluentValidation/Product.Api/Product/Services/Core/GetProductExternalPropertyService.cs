using Domain.Models;
using Google.Protobuf.WellKnownTypes;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.Domain;
using TXC.Proto.Product;

namespace Services.Core
{
    public class GetProductExternalPropertyService : IGetProductExternalPropertyService
    {
        private IDbConnection _dbConnection;
        public GetProductExternalPropertyService()
        {

        }
        public async Task<ProtoBaseResponse> GetProductExternalProperty(
            GetProductExternalPropertyRequest request
            , CheckProductDel _checkProductDel
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , GetExternalPropertyListDel _getExternalPropertyListDel
            )
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
                return new ProtoBaseResponse() { Success = false, Message = "Product not found" };
            }

            IEnumerable<ExternalPropertyItem> _restrictionSet = await _getExternalPropertyListDel(request.ProductId, _dbConnection);
            GetProductExternalPropertyResponse response = new GetProductExternalPropertyResponse { ProductId = request.ProductId };
            response.ExternalPropertyListItems.AddRange(_restrictionSet);

            return new ProtoBaseResponse
            {
                Success = true,
                Message = "Success",
                Data = Any.Pack(response)
            };
        }
    }
}
