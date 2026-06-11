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
using Google.Protobuf.WellKnownTypes;
using Services.Interface;
using Dapper;
using Domain.Dto;
using TXC.Common.MessageContract.Product;

namespace Services.Core
{
    public class GetProductBrandService : IGetProductBrandService
    {
        private IDbConnection _dbConnection;

        public GetProductBrandService()
        {
        }

        public async Task<ProtoBaseResponse> GetProductBrand(
            GetProductBrandRequest request
            , GetConfigDel getConfig
            , GetDBConnectionDel getDBConnection
            , GetProductBrandDel getProductBrand
            , CheckProductDel _checkProductDel)
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

                if (productChecker.Item2 != (int)MultipleSelectionType.General && productChecker.Item2 != (int)MultipleSelectionType.Child)
                {
                    return new ProtoBaseResponse() { Success = false, Message = "Product type invalid" };
                }

                BrandDto _brand = await getProductBrand(request, _dbConnection);

                GetProductBrandResponse response = new GetProductBrandResponse();
                if (_brand == null)
                {
                    return new ProtoBaseResponse() { Success = false, Message = "Product does not exist" };
                }

                response.BrandId = _brand.BrandId;
                response.BrandName = _brand.BrandName;
                response.BrandStatus = _brand.BrandStatus;

                return new ProtoBaseResponse() { Success = true, Message = "Success", Data = Any.Pack(response) };
            }
            catch (Exception ex) 
            {
                return new ProtoBaseResponse() { Success = false, Message = ex.Message };
            }
        }
    }
}
