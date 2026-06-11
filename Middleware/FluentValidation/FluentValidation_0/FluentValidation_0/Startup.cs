using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

namespace FluentValidation_0
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
            services.AddControllers();

            services.AddSingleton<IStudentService, StudentService>();
            //inject validator
            services.AddSingleton<AbstractValidator<QueryStudentHobbiesDto>, QueryStudentHobbiesDtoValidator>();
            services.AddSingleton<IValidator<QueryStudentHobbiesDto>, QueryStudentHobbiesDtoValidator>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    //;
                    //when using CustomizeValidator, should add the following code.
                    .AddFluentValidation(fv =>
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                        //fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                        //fv.ImplicitlyValidateChildProperties = true;
                    });

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1",
                new OpenApiInfo { Title = "WebApp", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp");
            });
        }
    }
}
