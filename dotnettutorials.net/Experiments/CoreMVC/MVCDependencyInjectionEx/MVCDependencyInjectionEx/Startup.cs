using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCDependencyInjectionEx.Models;

namespace MVCDependencyInjectionEx
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

            //Application Service
            services.Add(new ServiceDescriptor(typeof(IStudentRepository), new TestStudentRepository())); // by default singleton
            services.Add(new ServiceDescriptor(typeof(IStudentRepository), typeof(TestStudentRepository), ServiceLifetime.Singleton)); // singleton
            services.Add(new ServiceDescriptor(typeof(IStudentRepository), typeof(TestStudentRepository), ServiceLifetime.Transient)); // Transient
            services.Add(new ServiceDescriptor(typeof(IStudentRepository), typeof(TestStudentRepository), ServiceLifetime.Scoped));    // Scoped
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
