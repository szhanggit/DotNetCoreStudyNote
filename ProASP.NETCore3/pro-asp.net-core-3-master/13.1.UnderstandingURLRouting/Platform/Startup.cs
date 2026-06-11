using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Platform {
    public class Startup {

        public void ConfigureServices(IServiceCollection services) {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapGet("routing", async context => {
                    await context.Response.WriteAsync("Request Was Routed");
                });
                endpoints.MapGet("capital/uk", new Capital().Invoke);
                endpoints.MapGet("population/paris", new Population().Invoke);
            });

            app.Use(async (context, next) => {
                await context.Response.WriteAsync("Terminal Middleware Reached");
            });
        }
        //http://localhost:5000/routing
        //http://localhost:5000/notrouted
        //http://localhost:5000/capital/uk
        //http://localhost:5000/population/paris
    }
}
