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
    
    public delegate Task<bool> SendProductUpdateStatusMessageDel(int TenantId, string queueNameConfig, UpdateProductStatusV1 message);       
    public delegate Task<int> UpdateProductStatusDel(UpdateProductStatusRequest request, IDbConnection _dbConnection);    
    public delegate Task<Tuple<int, int>> CheckProductDel(int ProductId, IDbConnection _dbConnection);
    public delegate Task<bool> CheckContractDel(int ProductId, IDbConnection _dbConnection);
    public delegate Task<bool> CheckReverseLimitNameDel(int ReverseLimitNameId, IDbConnection _dbConnection);

    public interface ICommonProductService
    {        
        Task<bool> SendProductUpdateStatusMessage(int TenantId, string queueNameConfig, UpdateProductStatusV1 message);                
        Task<int> UpdateProductStatus(UpdateProductStatusRequest request, IDbConnection _dbConnection);        
        Task<Tuple<int, int>> CheckProductId(int ProductId, IDbConnection _dbConnection);
        Task<bool> CheckContract(int ProductId, IDbConnection _dbConnection);
        Task<bool> CheckReverseLimitName(int ReverseLimitNameId, IDbConnection _dbConnection);
    }
}
