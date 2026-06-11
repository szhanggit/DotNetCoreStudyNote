using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXC.Common.Services.KeyVault
{
    public static class KeyVaultKeyGenerator
    {
        public static string ProgramCollectionCodeSecretKey(string programCollectionCode, bool isReadReplica)
        {
            var readSuffix = isReadReplica ? "Read" : "";

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Production"))
                return $"ProgramCollectionConnectionString{readSuffix}-{programCollectionCode}";
            else
                return $"{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}-ProgramCollectionConnectionString{readSuffix}-{programCollectionCode}";
        }
        public static string TenantDbConnectionStringSecretKey(int tenantId, bool isReadReplica)
        {
            var readSuffix = isReadReplica ? "Read" : "";

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").Equals("Production"))
                return $"TenantDBConnectionString{readSuffix}-{tenantId}";
            else
                return $"{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}-TenantDBConnectionString{readSuffix}-{tenantId}";
        }
    }
}
