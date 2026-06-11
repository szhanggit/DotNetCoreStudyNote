using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Interface
{
    public interface ISelectProductsService
    {
        public Task<ProtoBaseResponse> GetProducts(GetProductsRequest request);        
    }
}
