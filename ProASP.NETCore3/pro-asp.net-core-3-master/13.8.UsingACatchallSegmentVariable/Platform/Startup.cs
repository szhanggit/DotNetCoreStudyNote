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
				/*
					pattern segments that contain multiple variables are matched from right to left.
				*/
				endpoints.MapGet("{first}/{second}/{*catchall}", async context => {
					await context.Response.WriteAsync("Request Was Routed\n");
					foreach (var kvp in context.Request.RouteValues)
					{
						await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
					}
				});
				endpoints.MapGet("capital/{country=France}", Capital.Endpoint);
				endpoints.MapGet("size/{city?}", Population.Endpoint).WithMetadata(new RouteNameMetadata("population"));
			});/*Optional segments are denoted with a question mark (the ? character) after the variable name and allow the route to match
				URLs that don’t have a corresponding path segment,*/

			app.Use(async (context, next) => {
				await context.Response.WriteAsync("Terminal Middleware Reached");
			});
		}
		//http://localhost:5000/one/two/three/four
	}
}


/*
Request Was Routed
first: one
second: two
catchall: three/four 
	 */
