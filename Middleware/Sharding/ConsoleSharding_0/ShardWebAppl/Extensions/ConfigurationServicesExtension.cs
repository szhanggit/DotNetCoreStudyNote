using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Command.Shard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TXC.Common.Data.Core;
using TXC.Common.Data.TenantDbConnection;

namespace ShardWebAppl.Extensions
{
    public static class ConfigurationServicesExtension
    {
        public static void ConfigureDataOperations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDapperOperation>(dapper => new DapperOperation());

            services.AddScoped<ITenantDbConnection, TenantDbConnection>();
            services.Configure<ShardConfiguration>(options => configuration.GetSection("ShardConfiguration").Bind(options));
            services.AddSingleton<ITenantShardMapHelper, TenantShardMapHelper>();

            services.AddScoped<IDbCommand>(cmd => new SqlCommand { CommandTimeout = Convert.ToInt32(configuration.GetSection("SqlCommand:CommandTimeout").Value) });
        }

        public static void ConfigureMediateR(this IServiceCollection services)
        {

            services.AddMediatR(typeof(CreateShardCommand).Assembly);
        }
    }
}
