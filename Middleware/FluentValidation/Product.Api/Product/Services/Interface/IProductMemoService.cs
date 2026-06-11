using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.MessageContract.Product;
using TXC.Proto.Product;

namespace Services.Interface
{
    public delegate Task<int> UpdateProductMemoDel(UpdateProductMemoRequest request, IDbConnection _dbConnection);
    public delegate Task<ProductMemo> GetProductMemoDel(int ProductId, IDbConnection _dbConnection);
    public delegate Task<bool> SendProductUpdateMemoMessageDel(int TenantId, string queueNameConfig, UpdateProductMemoV1 message);
    public interface IProductMemoService
    {
        Task<int> UpdateProductMemo(UpdateProductMemoRequest request, IDbConnection _dbConnection);
        Task<ProductMemo> GetProductMemo(int ProductId, IDbConnection _dbConnection);
        Task<bool> SendProductUpdateMemoMessage(int TenantId, string queueNameConfig, UpdateProductMemoV1 message);
    }
}
