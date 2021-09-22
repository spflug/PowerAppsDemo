using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PowerApps.Implementations;
using PowerApps.Interfaces;
#pragma warning disable 1591

namespace PowerApps
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Starwars characters and planets", Version = "v1",
                    Description = "Provides an API to search for Starwars character and planet information.",
                    Contact = new OpenApiContact
                    {
                        Email = "simon.pflughoft@adesso.de",
                        Url = new Uri("https://adesso.de"),
                        Name = "adesso SE"
                    }
                });
                c.IncludeXmlComments(@"PowerApps.xml");
            });
            services.AddMemoryCache();
            services.AddSingleton<ICachingClient, CachingClient>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(c =>
                {
                    c.SerializeAsV2 = true;
                });
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PowerApps v1");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}