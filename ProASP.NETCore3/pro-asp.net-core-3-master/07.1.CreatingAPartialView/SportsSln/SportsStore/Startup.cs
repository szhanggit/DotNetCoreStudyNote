using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        private IConfiguration Configuration { get; set; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            /*
             Entity Framework Core must be configured so that it knows the type of database to which it will connect, which connection string
             describes that connection, and which context class will present the data in the database.
             */
            services.AddDbContext<StoreDbContext>(opts => {
                opts.UseSqlServer(
                Configuration["ConnectionStrings:SportsStoreConnection"]);
            });

            services.AddScoped<IStoreRepository, EFStoreRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                /*
                This extension method displays details of exceptions that occur in the application, which is
                useful during the development process, as described in Chapter 16. It should not be enabled in
                deployed applications, and I disable this feature when I prepare the SportsStore application for
                deployment in Chapter 11.    
             */
            }
            app.UseStatusCodePages(); /*This extension method adds a simple message to HTTP responses that would not otherwise
have a body, such as 404 - Not Found responses. This feature is described in Chapter 16.*/
            app.UseStaticFiles();/*This extension method enables support for serving static content from the wwwroot folder. I
describe the support for static content in Chapter 15.*/

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute("pagination", "Products/Page{productPage}", new { Controller = "Home", action = "Index" });
                endpoints.MapDefaultControllerRoute();
            });

            SeedData.EnsurePopulated(app);
        }
    }
}

/*
 http://localhost:5000/?productPage=2
 http://localhost:5000/Products/Page2
     */
