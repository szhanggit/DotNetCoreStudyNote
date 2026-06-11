using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp {
    public class Startup {

        public Startup(IConfiguration config) {
            Configuration = config;
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<DataContext>(opts => {
                opts.UseSqlServer(Configuration[
                    "ConnectionStrings:ProductConnection"]);
                opts.EnableSensitiveDataLogging(true);
            });
            //services.AddControllers();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();    

            services.AddDistributedMemoryCache();               //just added
            services.AddSession(options => {                    //just added
                options.Cookie.IsEssential = true;
            });
        }

        public void Configure(IApplicationBuilder app, DataContext context) {
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSession();                                   //just added
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                //endpoints.MapControllerRoute("Default","{controller=Home}/{action=Index}/{id?}");
                endpoints.MapDefaultControllerRoute();          //The MapDefaultControllerRoute method avoids the risk of mistyping the URL pattern and sets up the convention-based routing.
            });
            SeedData.SeedDatabase(context);
        }
    }
}
