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
    public delegate Task<int> InsertExternalPropertyDel(List<ProductExternalPropertySet> _inputList, int ProductId, string UserName, IDbConnection _dbConnection);
    public delegate Task<IEnumerable<ExternalPropertyItem>> GetExternalPropertyListDel(int ProductId, IDbConnection _dbConnection);
    public delegate Task<int> DeleteExternalPropertyDel(int ProductId, string UserName, IDbConnection _dbConnection);
    public delegate Task<bool> SendProductCreateExternalPropertyMessageDel(int TenantId, string queueNameConfig, CreateProductExternalPropertyV1 message);
    public delegate Task<bool> SendProductDeleteExternalPropertyMessageDel(int TenantId, string queueNameConfig, CreateProductExternalPropertyV1 message);
    public interface IExternalPropertyService
    {
        Task<int> InsertExternalProperty(List<ProductExternalPropertySet> _inputList, int ProductId, string UserName, IDbConnection _dbConnection);
        Task<IEnumerable<ExternalPropertyItem>> GetExternalPropertyList(int ProductId, IDbConnection _dbConnection);
        Task<int> DeleteExternalProperty(int ProductId, string UserName, IDbConnection _dbConnection);
        Task<bool> SendProductCreateExternalPropertyMessage(int TenantId, string queueNameConfig, CreateProductExternalPropertyV1 message);
        Task<bool> SendProductDeleteExternalPropertyMessage(int TenantId, string queueNameConfig, CreateProductExternalPropertyV1 message);
    }
}
