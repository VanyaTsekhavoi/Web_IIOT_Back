using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Web_IIOT_Back.Project_Configuration;
using Web_IIOT_Back.Services;

namespace Web_IIOT_Back
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

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(sw =>
            {
                sw.SwaggerDoc("v1", new     OpenApiInfo
                { Title = "Core API", Description = "Web Service For IIOT Maquette", Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Ivan Tsekhavoi",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/VanyaTsekhavoi"),
                    },
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + @"Web_IIOT_Back.xml";
                sw.IncludeXmlComments(xmlPath);

                
            }
            );

            services.AddSingleton<IBoxService, BoxService>();
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

            var swaggerInitialConfiguration = new SwaggerConfiguration();
            Configuration.GetSection(nameof(SwaggerConfiguration)).Bind(swaggerInitialConfiguration);

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(sw =>
            {
                sw.RouteTemplate = swaggerInitialConfiguration.JsonRoute;
            });
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(sw =>
            {
                sw.SwaggerEndpoint(swaggerInitialConfiguration.UiEndPoint, swaggerInitialConfiguration.ApiDescription);
                sw.RoutePrefix = string.Empty;
            });
        }
    }
}
