using Domain.Models.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShardWebAppl.Extensions
{
    public static class ShardMapHelperExtension
    {
        public static IServiceCollection ConfigureShardMapHelper(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GlobalShardConfiguration>(options => configuration.GetSection("GlobalShardConfiguration").Bind(options));
            var sh = new ShardMapHelper(configuration);
            services.AddSingleton<IShardMapHelper>(sh);
            return services;
        }
    }
}
