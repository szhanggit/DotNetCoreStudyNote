using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TXC.Common.Domain;

namespace TXC.Common.Data.TenantDbConnection
{
    public interface ITenantDbConnection
    {
        Task<Response<IDbConnection>> GetTenantDbConnection(string tenantId, bool isReadReplica, CancellationToken cancellationToken);
        Task<Response<IDbConnection>> GetTenantDbConnection(bool isReadReplica, CancellationToken cancellationToken);
    }
}
