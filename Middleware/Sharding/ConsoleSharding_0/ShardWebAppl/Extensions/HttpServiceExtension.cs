using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TXC.Common.Services.HttpService;

namespace ShardWebAppl.Extensions
{
    public static class HttpServiceExtension
    {
        public static IServiceCollection AddHttpService(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddScoped<IHttpService, HttpService>();
            return services;
        }
    }
}
