using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.Domain;
using TXC.Common.MessageContract.Product;
using TXC.Proto.Product;

namespace Services.Interface
{
    public delegate Task<Tuple<int, int>> CheckBrandDel(int? BrandId, IDbConnection _dbConnection);
    public delegate Task<bool> SendProductUpdateBrandMessageDel(int TenantId, string queueNameConfig, UpdateProductBrandV1 message);
    public delegate Task<int> UpdateProductBrandDel(UpdateProductBrandRequest request, IDbConnection _dbConnection);
    public delegate Task<BrandDto> GetProductBrandDel(GetProductBrandRequest request, IDbConnection _dbConnection);
    public interface IProductBrandService
    {
        Task<Tuple<int, int>> CheckBrandId(int? BrandId, IDbConnection _dbConnection);
        Task<bool> SendProductUpdateBrandMessage(int TenantId, string queueNameConfig, UpdateProductBrandV1 message);
        Task<int> UpdateProductBrand(UpdateProductBrandRequest request, IDbConnection _dbConnection);
        Task<BrandDto> GetProductBrand(GetProductBrandRequest request, IDbConnection _dbConnection);
    }
}
