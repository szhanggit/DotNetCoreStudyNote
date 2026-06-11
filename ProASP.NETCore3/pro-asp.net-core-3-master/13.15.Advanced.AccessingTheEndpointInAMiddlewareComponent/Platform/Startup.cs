using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;

namespace Platform {
    public class Startup {

        public void ConfigureServices(IServiceCollection services) {
			services.Configure<RouteOptions>(opts => {
				opts.ConstraintMap.Add("countryName", typeof(CountryRouteConstraint));
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseDeveloperExceptionPage();
			app.UseRouting();

			app.Use(async (context, next) => {
				Endpoint end = context.GetEndpoint();
				if (end != null)
				{
					await context.Response.WriteAsync($"{end.DisplayName} Selected \n");
				}
				else
				{
					await context.Response.WriteAsync("No Endpoint Selected \n");
				}
				await next();
			});

			app.UseEndpoints(endpoints => {
				endpoints.Map("{number:int}", async context => {
					await context.Response.WriteAsync("Routed to the int endpoint");
				}).WithDisplayName("Int Endpoint").Add(b => ((RouteEndpointBuilder)b).Order = 1);
				endpoints.Map("{number:double}", async context => {
					await context.Response.WriteAsync("Routed to the double endpoint");
				}).WithDisplayName("Double Endpoint").Add(b => ((RouteEndpointBuilder)b).Order = 2);
			});

			app.Use(async (context, next) => {
				await context.Response.WriteAsync("Terminal Middleware Reached");
			});
		}
		//http://localhost:5000/23.5
		//http://localhost:5000/23
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
