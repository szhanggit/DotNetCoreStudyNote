using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;

namespace TXC.Common.Data.TenantDbConnection
{
    public interface ITenantShardMapHelper
    {
        public ListShardMap<int> GetListShardMap();
    }
}
