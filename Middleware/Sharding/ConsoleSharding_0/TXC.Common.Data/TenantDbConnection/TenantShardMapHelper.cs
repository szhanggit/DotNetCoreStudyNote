using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
using Microsoft.Extensions.Options;

namespace TXC.Common.Data.TenantDbConnection
{
    public class TenantShardMapHelper : ITenantShardMapHelper
    {
        private readonly ShardConfiguration _config;
        private ListShardMap<int> _lss;
        public TenantShardMapHelper(IOptions<ShardConfiguration> config)
        {
            _config = config.Value;
        }
        public ListShardMap<int> GetListShardMap()
        {
            if (_lss != null)
                return _lss;

            if (string.IsNullOrEmpty(_config.GlobalShardMapConnectionString))
                throw new System.Exception("Missing GlobalShardMapConnectionString");

            ShardMapManager smm = ShardMapManagerFactory.GetSqlShardMapManager(_config.GlobalShardMapConnectionString, ShardMapManagerLoadPolicy.Lazy);
            ListShardMap<int> tenantShardMap = smm.GetListShardMap<int>(_config.ShardMapName);

            return tenantShardMap;
        }
    }
}
