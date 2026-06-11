using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.Interface
{
    public interface IUpdateProductAcceptanceLoopService
    {
        Task<ProtoBaseResponse> UpdateProductAcceptanceLoop(UpdateProductAcceptanceLoopRequest request);
    }
}
