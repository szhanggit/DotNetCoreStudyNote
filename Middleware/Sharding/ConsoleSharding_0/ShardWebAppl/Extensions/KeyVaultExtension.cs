using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TXC.Common.Services.KeyVault;

namespace ShardWebAppl.Extensions
{
    public static class KeyVaultExtension
    {
        public static IServiceCollection AddKeyVault(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IKeyVaultServices, KeyVaultService>();
            return services;
        }
    }
}
