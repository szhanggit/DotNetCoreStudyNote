using Google.Protobuf.WellKnownTypes;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Core
{
    public class SelectProductsService : ISelectProductsService
    {
        public SelectProductsService()
        {

        }

        public async Task<ProtoBaseResponse> GetProducts(GetProductsRequest request)
        {
            try 
            {
                IEnumerable<GetProductsItem> result = null;
                GetProductsResponse response = new GetProductsResponse();
                response.TotalCount = 130;
                response.ProductListItems.AddRange(result);

                return new ProtoBaseResponse
                {
                    Success = true,
                    Message = "Success",
                    Data = Any.Pack(response)
                };
            } 
            catch (Exception ex) 
            {
                return new ProtoBaseResponse() { Success = false, Message = ex.Message };
            }
        }
    }
}
