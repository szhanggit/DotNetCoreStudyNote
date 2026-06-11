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
				endpoints.MapGet("{first:alpha:length(3)}/{second:bool}", async context => {
					await context.Response.WriteAsync("Request Was Routed\n");
					foreach (var kvp in context.Request.RouteValues)
					{
						await context.Response.WriteAsync($"{kvp.Key}: {kvp.Value}\n");
					}
				});
				endpoints.MapGet("capital/{country:regex(^uk|france|monaco$)}", Capital.Endpoint);
				endpoints.MapGet("size/{city?}", Population.Endpoint).WithMetadata(new RouteNameMetadata("population"));

				endpoints.MapFallback(async context => {await context.Response.WriteAsync("Routed to fallback endpoint");
				});
			});/*Optional segments are denoted with a question mark (the ? character) after the variable name and allow the route to match
				URLs that don’t have a corresponding path segment,*/

			app.Use(async (context, next) => {
				await context.Response.WriteAsync("Terminal Middleware Reached");
			});
		}
		//http://localhost:5000/notmatched
	}
}


/*
alpha			 This constraint matches the letters a to z (and is case-insensitive).
bool 			 This constraint matches true and false (and is case-insensitive).
datetime 		 This constraint matches DateTime values, expressed in the nonlocalized invariant culture format.
decimal 		 This constraint matches decimal values, formatted in the nonlocalized invariant culture.
double 			 This constraint matches double values, formatted in the nonlocalized invariant culture.
file 			 This constraint matches segments whose content represents a file name, in the form name.ext. The existence of the file is not validated.
float 			 This constraint matches float values, formatted in the nonlocalized invariant culture.
guid 			 This constraint matches GUID values.
int 			 This constraint matches int values.
length(len) 	 This constraint matches path segments that have the specified number of characters.
length(min, max) This constraint matches path segments whose length falls between the lower and upper values specified.
long 			 This constraint matches long values.
max(val) 		 This constraint matches path segments that can be parsed to an int value that is less than or equal to the specified value.
maxlength(len)   This constraint matches path segments whose length is equal to or less than the specified value.
min(val) 		 This constraint matches path segments that can be parsed to an int value that is more than or equal to the specified value.
minlength(len) 	 This constraint matches path segments whose length is equal to or more than the specified value.
nonfile 		 This constraint matches segments that do not represent a file name, i.e., values that would not be matched by the file constraint.
range(min, max)  This constraint matches path segments that can be parsed to an int value that falls between the inclusive range specified.
regex(expression)This constraint applies a regular expression to match path segments.


MapFallback(endpoint)			This method creates a fallback that routes requests to an endpoint.
MapFallbackToFile(path)			This method creates a fallback that routes requests to a file.
	 */
