using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Platform {
    public class Startup {

        public void ConfigureServices(IServiceCollection services) {
        }

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseDeveloperExceptionPage();
			app.UseRouting();
			app.UseEndpoints(endpoints => {
				endpoints.MapGet("{first}/{second}/{third}", async context => {
					await context.Response.WriteAsync("Request Was Routed\n");
					foreach (var kvp in context.Request.RouteValues)
					{
						await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
						/*
							Count This property returns the number of segment variables.
							ContainsKey(key) This method returns true if the route data contains a value for the specified key.
					 */
					}
				});
				endpoints.MapGet("capital/uk", new Capital().Invoke);
				endpoints.MapGet("population/paris", new Population().Invoke);
			});

			app.Use(async (context, next) => {
				await context.Response.WriteAsync("Terminal Middleware Reached");
			});
		}
		//http://localhost:5000/apples/oranges/cherries
	}
}
