using Grpc.Core;
using Services.Interface;
using Services.Queries;
using System.Threading.Tasks;
using TXC.Proto.Product;

namespace Services.gRPCServices
{
    public class ProductService : Product.ProductBase
    {
        private readonly ISelectProductsService _selectProductsService;
        private readonly IGetProductByIdQuery _getProductByIdQuery;
        private readonly IUpdateProductAcceptanceLoopService _addProductAcceptanceLoopService;

        public ProductService(
            ISelectProductsService selectProductsService
            , IGetProductByIdQuery getProductByIdQuery
            , IUpdateProductAcceptanceLoopService addProductAcceptanceLoopService)
        {
            _selectProductsService = selectProductsService;
            _getProductByIdQuery = getProductByIdQuery;
            _addProductAcceptanceLoopService = addProductAcceptanceLoopService;
        }

        public override async Task<ProtoBaseResponse> GetProducts(GetProductsRequest request, ServerCallContext context)
        {
            return await _selectProductsService.GetProducts(request);
        }

        public override async Task<ProtoBaseResponse> GetProductById(GetProductByIdRequest request, ServerCallContext context)
        {
            return await _getProductByIdQuery.GetProductById(request);
        }

        public override async Task<ProtoBaseResponse> UpdateProductAcceptanceLoop(UpdateProductAcceptanceLoopRequest request, ServerCallContext context)
        {
            return await _addProductAcceptanceLoopService.UpdateProductAcceptanceLoop(request);
        }
    }
}
