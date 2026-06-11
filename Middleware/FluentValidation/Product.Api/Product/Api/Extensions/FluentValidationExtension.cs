using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Validators;

namespace Product.Api.Extensions
{
    public static class FluentValidationExtension
    {
        public static void AddFluentValidation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<AcceptanceLoopValidator>();
            });
        }
    }
}
