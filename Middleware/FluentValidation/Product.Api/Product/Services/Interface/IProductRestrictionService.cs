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
    public delegate Task<int> InsertTimeRestrictionDel(List<ProductRedeemTimeRestrictionSet> _inputList, int ProductId, string UserName, IDbConnection _dbConnection);
    public delegate Task<IEnumerable<ProductRedeemTimeRestrictionSet>> GetTimeRestrictionListDel(int ProductId, IDbConnection _dbConnection);
    public delegate Task<int> DeleteTimeRestrictionDel(int ProductId, string UserName, IDbConnection _dbConnection);
    public delegate Task<int> InsertDateRestrictionDel(List<DateTime> _inputList, int ProductId, string UserName, IDbConnection _dbConnection);
    public delegate Task<IEnumerable<ProductRedeemDateRestrictionSet>> GetDateRestrictionListDel(int ProductId, IDbConnection _dbConnection);
    public delegate Task<int> DeleteDateRestrictionDel(int ProductId, string UserName, IDbConnection _dbConnection);
    public delegate Task<bool> SendProductCreateRestrictionMessageDel(int TenantId, string queueNameConfig, CreateProductRestrictionV1 message);
    public delegate Task<bool> SendProductDeleteRestrictionMessageDel(int TenantId, string queueNameConfig, CreateProductRestrictionV1 message);
    public interface IProductRestrictionService
    {
        Task<int> InsertTimeRestriction(List<ProductRedeemTimeRestrictionSet> _inputList, int ProductId, string UserName, IDbConnection _dbConnection);
        Task<IEnumerable<ProductRedeemTimeRestrictionSet>> GetTimeRestrictionList(int ProductId, IDbConnection _dbConnection);
        Task<int> DeleteTimeRestriction(int ProductId, string UserName, IDbConnection _dbConnection);
        Task<int> InsertDateRestriction(List<DateTime> _inputList, int ProductId, string UserName, IDbConnection _dbConnection);
        Task<IEnumerable<ProductRedeemDateRestrictionSet>> GetDateRestrictionList(int ProductId, IDbConnection _dbConnection);
        Task<int> DeleteDateRestriction(int ProductId, string UserName, IDbConnection _dbConnection);
        Task<bool> SendProductCreateRestrictionMessage(int TenantId, string queueNameConfig, CreateProductRestrictionV1 message);
        Task<bool> SendProductDeleteRestrictionMessage(int TenantId, string queueNameConfig, CreateProductRestrictionV1 message);
    }
}
