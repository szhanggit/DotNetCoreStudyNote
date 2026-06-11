using Domain.Models.Configuration;
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
using Microsoft.Extensions.Options;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TXC.Common.Domain;
using TXC.Common.Services.Wrappers;

namespace Services.Command.Shard
{
    public class CreateShardCommandHandler : IRequestHandlerWrapper<CreateShardCommand, bool>
    {
        private readonly ShardMapManager _smHelper;
        private readonly GlobalShardConfiguration _shardConfig;
        public CreateShardCommandHandler(IShardMapHelper smHelper, IOptions<GlobalShardConfiguration> shardConfig)
        {
            _smHelper = smHelper.GetShardMap();
            _shardConfig = shardConfig.Value;
        }


        public Task<Response<bool>> Handle(CreateShardCommand request, CancellationToken cancellationToken)
        {
            // Try to get a reference to the Shard Map.
            ListShardMap<int> shardMap;
            bool shardMapExists = _smHelper.TryGetListShardMap(_shardConfig.ShardMapName, out shardMap);

            if (!shardMapExists)
            {
                // The Shard Map does not exist, so create it
                shardMap = _smHelper.CreateListShardMap<int>(_shardConfig.ShardMapName);
            }

            try
            {
                // Add a new shard to hold the range being added.
                var shardServer = _shardConfig.ShardServer;
                Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement.Shard shard = null;

                if (!shardMap.TryGetShard(new ShardLocation(shardServer, request.DbName), out shard))
                {
                    shard = shardMap.CreateShard(new ShardLocation(shardServer, request.DbName));
                }

                // Create the mapping and associate it with the new shard
                shardMap.CreatePointMapping(new PointMappingCreationInfo<int>(request.Key, shard, MappingStatus.Online));
            }
            catch (Exception e)
            {
                return Task.FromResult(Response.Fail<bool>("Fail", false));
            }

            return Task.FromResult(Response.Success<bool>("Success", true));
        }
    }
}
