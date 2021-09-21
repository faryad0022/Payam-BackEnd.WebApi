using BackEnd.Core.Services.Implementations;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Extensions.Connection;
using BackEnd.DataLayer.Repository;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(
                new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json")
                .Build()
                );


            #region Add DbContext
            services.AddApplicationDbContext(Configuration);
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion

            #region Application Services
            services.AddScoped<IUserService, UserService>();
            #endregion


            services.AddControllers();
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

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template:"{controller=users}/{action=users}/{is?}"
            //        );
            //});
        }
    }
}
