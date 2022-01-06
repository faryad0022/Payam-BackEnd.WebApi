using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.utilities.Extensions.StartUpConfigurations
{
    public static class CorsConfiguration
    {
        public static void AddCorse(this IServiceCollection services, IConfiguration Configuration) 
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Configuration["Cors:PolicyString"], builder =>
                {
                    builder.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .WithExposedHeaders("ejUrl").WithExposedHeaders("ejUrlName")
                    //.AllowCredentials()
                    .Build();
                });
            });
        }

        public static void UseCorse(this IApplicationBuilder app, IConfiguration Configuration)
        {
            app.UseCors(Configuration["Cors:PolicyString"]);

        }
    }
}
