using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Core;
using Services.Interface;
using Services.Queries;

namespace Product.Api.Extensions
{
    public static class GRPCServiceExtension
    {
        public static IServiceCollection AddgRPCService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISelectProductsService, SelectProductsService>();
            services.AddScoped<IGetProductByIdQuery, GetProductByIdQuery>();
            services.AddScoped<IUpdateProductAcceptanceLoopService, UpdateProductAcceptanceLoopService>();

            return services;
        }
    }
}
