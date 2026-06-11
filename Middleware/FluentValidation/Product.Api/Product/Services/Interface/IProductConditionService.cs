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
    public delegate Task<ProductConditionResponse> GetProductConditionDel(int ProductId, IDbConnection _dbConnection);
    public delegate Task<int> UpdateProductConditionDel(ProductUpdatingConditionRequest request, IDbConnection _dbConnection);
    public delegate Task<bool> SendProductConditionMessageDel(int TenantId, string queueNameConfig, UpdateProductConditionV1 message);
    public delegate bool CompareConditionIsTheSameDel(ProductCondition item1, ProductCondition item2);
    public interface IProductConditionService
    {
        Task<ProductConditionResponse> GetProductCondition(int ProductId, IDbConnection _dbConnection);
        Task<int> UpdateProductCondition(ProductUpdatingConditionRequest request, IDbConnection _dbConnection);
        Task<bool> SendProductConditionMessage(int TenantId, string queueNameConfig, UpdateProductConditionV1 message);
        bool CompareConditionIsTheSame(ProductCondition item1, ProductCondition item2);
    }
}
