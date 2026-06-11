using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TXCFluValidation.Validators;

namespace TXCFluValidation.Extensions
{
    public static class FluentValidationExtension
    {
        public static void AddFluentValidation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssemblyContaining<ProductValidtor>();
            services.AddValidatorsFromAssemblyContaining<SKUValidator>();
            services.AddFluentValidationAutoValidation();
        }
    }
}
