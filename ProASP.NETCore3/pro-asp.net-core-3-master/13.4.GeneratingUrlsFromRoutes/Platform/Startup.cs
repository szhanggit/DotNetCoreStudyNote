using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

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
				endpoints.MapGet("capital/{country}", Capital.Endpoint);
				endpoints.MapGet("size/{city}", Population.Endpoint).WithMetadata(new RouteNameMetadata("population"));//Basically it is a route name.
				/*
				 (P292)	The WithMetadata method is used on the result from the MapGet method to assign metadata to the route. The only metadata
required for generating URLs is a name, which is assigned by passing a new RouteNameMetadata object, whose constructor argument
specifies the name that will be used to refer to the route.
				 */
			});

			app.Use(async (context, next) => {
				await context.Response.WriteAsync("Terminal Middleware Reached");
			});
		}
		//http://localhost:5000/capital/monaco
	}
}
