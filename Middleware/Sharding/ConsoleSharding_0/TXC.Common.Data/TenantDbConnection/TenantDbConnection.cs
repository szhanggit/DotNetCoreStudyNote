using Microsoft.AspNetCore.Http;
using Microsoft.Azure.SqlDatabase.ElasticScale.ShardManagement;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using TXC.Common.Domain;
using TXC.Common.Services.KeyVault;
using TXC.Common.Domain.Models;
using Dapper;

namespace TXC.Common.Data.TenantDbConnection
{
    public class TenantDbConnection : ITenantDbConnection, IDisposable
    {
        private readonly ITenantShardMapHelper _shardHelper;
        private string _tenantId;
        private readonly IKeyVaultServices _keyvaultServices;
        private IDbConnection existingConnection = null;

        public TenantDbConnection(ITenantShardMapHelper shardHelper, IHttpContextAccessor httpContextAccessor, IKeyVaultServices keyVaultServices)
        {
            _shardHelper = shardHelper;
            _tenantId = httpContextAccessor.HttpContext?.Request?.Headers["TenantBasicInfoId"];
            _keyvaultServices = keyVaultServices;
        }

        public async Task<Response<IDbConnection>> GetTenantDbConnection(string tenantId, bool isReadReplica, CancellationToken cancellationToken)
        {
            _tenantId = tenantId;
            return await GetTenantDbConnection(isReadReplica, cancellationToken);
        }

        public async Task<Response<IDbConnection>> GetTenantDbConnection(bool isReadReplica, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(_tenantId))
                return Response.Fail<IDbConnection>("Missing TenantId Header", null);

            if (!int.TryParse(_tenantId, out int tenantId))
                return Response.Fail<IDbConnection>("TenantId should be int", null);

            if (existingConnection != null)
                return Response.Success("Success", existingConnection);

            var credentialConnString = "";

            /*
             C:\Users\zhang\AppData\Roaming\Microsoft\UserSecrets\0747e3dd-48cf-41c5-a1af-a61fe82fd862
             */
            if (isReadReplica)
                credentialConnString = _keyvaultServices.GetKeyVaultSecret(KeyVaultKeyGenerator.TenantDbConnectionStringSecretKey(tenantId, true));
            else
                credentialConnString = _keyvaultServices.GetKeyVaultSecret(KeyVaultKeyGenerator.TenantDbConnectionStringSecretKey(tenantId, false));
            /*   "User ID=steven;Password=steven"   */
            // otherwise, use normal sharding
            try
            {
                var _listShardMap = _shardHelper.GetListShardMap();
                /*   https://www.mvndoc.com/c/com.microsoft.azure/azure-elasticdb-tools/com/microsoft/azure/elasticdb/shard/map/ShardMap.html   */
                existingConnection = await _shardHelper.GetListShardMap().OpenConnectionForKeyAsync(tenantId, credentialConnString, ConnectionOptions.Validate);

                /*Start: Test*/
                var list = existingConnection.Query<DictionaryInfo>("select dictionary_id as DictionaryId, category as Category, display_name as DisplayName from [general].[tb_d_dictionary] with(nolock)").ToList();
                /*End: Test*/

                return Response.Success<IDbConnection>("Success", existingConnection);
            }
            catch (Exception ex)
            {
                return Response.Fail<IDbConnection>(ex.Message, null);
            }
        }

        public void Dispose()
        {
            if (existingConnection != null)
                existingConnection.Dispose();
        }
    }
}
