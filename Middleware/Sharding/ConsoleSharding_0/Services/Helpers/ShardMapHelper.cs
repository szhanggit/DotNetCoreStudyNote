using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
using Microsoft.Extensions.Configuration;

namespace Services.Helpers
{
    public class ShardMapHelper : IShardMapHelper
    {
        private readonly string connectionString;
        private ShardMapManager _shardMapManager;
        public ShardMapHelper(IConfiguration configuration)
        {
            connectionString = configuration.GetSection("Tenant-ConnectionString:DefaultConnection").Value;
            //initialize it on startup
            GetShardMap();
        }

        public ShardMapManager GetShardMap()
        {
            if (_shardMapManager != null)
                return _shardMapManager;

            // Try to get a reference to the Shard Map Manager via the Shard Map Manager database.  
            // If it doesn't already exist, then create it.
            bool shardMapManagerExists = ShardMapManagerFactory.TryGetSqlShardMapManager(
                                                    connectionString,
                                                    ShardMapManagerLoadPolicy.Lazy,
                                                    out _shardMapManager);

            if (!shardMapManagerExists)
            {
                // The connectionString contains server name, database name, and admin credentials for privileges on both the GSM and the shards themselves.
                // Create the Shard Map Manager.
                ShardMapManagerFactory.CreateSqlShardMapManager(connectionString);

                _shardMapManager = ShardMapManagerFactory.GetSqlShardMapManager(
                        connectionString,
                        ShardMapManagerLoadPolicy.Lazy);
            }

            return _shardMapManager;
        }
    }
}
