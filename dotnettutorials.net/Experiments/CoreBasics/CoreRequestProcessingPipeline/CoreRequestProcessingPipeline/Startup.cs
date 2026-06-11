using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreRequestProcessingPipeline
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware1: Incoming Request\n");
                await next();
                await context.Response.WriteAsync("Middleware1: Outgoing Response\n");
            });
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Middleware2: Incoming Request\n");
                await next();
                await context.Response.WriteAsync("Middleware2: Outgoing Response\n");
            });
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Middleware3: Incoming Request handled and response generated\n");
            });
        }
    }
}
