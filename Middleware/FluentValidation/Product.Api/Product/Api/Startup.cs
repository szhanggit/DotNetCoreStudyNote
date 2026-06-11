using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Api.Extensions;
using Services.gRPCServices;
using Calzolari.Grpc.AspNetCore.Validation;

namespace Product.Api
{
    public class Startup
    {
        const string CORS_NAME = "product_service_CORSE_NAME";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers();

            services.AddFluentValidation(Configuration);
            #region gRPC
            services.AddGrpc(options =>
            {
                options.EnableMessageValidation();
            });
            services.AddGrpcValidation();
            services.AddGrpcReflection();
            #endregion gRPC            

            services.AddgRPCService(Configuration);

            //services.AddGrpc(options =>
            //{
            //    options.EnableMessageValidation();
            //});
            //services.AddGrpcValidation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ProductService>();
                endpoints.MapGrpcReflectionService();
                endpoints.MapControllers();
            });
        }
    }
}
