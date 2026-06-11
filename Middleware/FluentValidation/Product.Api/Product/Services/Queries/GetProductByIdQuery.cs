using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Queries
{
    public interface IGetProductByIdQuery
    {
        public Task<ProtoBaseResponse> GetProductById(GetProductByIdRequest request);

    }
    public class GetProductByIdQuery : IGetProductByIdQuery
    {
        public GetProductByIdQuery()
        {

        }

        public async Task<ProtoBaseResponse> GetProductById(GetProductByIdRequest request)
        {
            GetProductByIdResponse response = new GetProductByIdResponse();
            return new ProtoBaseResponse
            {
                Success = true,
                Message = "Success",
                Data = Any.Pack(response)
            };
        }
    }
}
