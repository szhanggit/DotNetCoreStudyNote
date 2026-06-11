using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TXC.Common.Domain;

namespace Services.Interface
{
    public delegate Task<TenantConfig> GetConfigDel(string ConfigName, int TenantId);
    public delegate Task<Response<IDbConnection>> GetDBConnectionDel(int TenantId);
    public interface ICoreService
    {
        Task<TenantConfig> GetConfig(string ConfigName, int TenantId);
        Task<Response<IDbConnection>> GetDBConnection(int TenantId);
    }
}
