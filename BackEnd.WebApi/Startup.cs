using BackEnd.Core.Security;
using BackEnd.Core.Services.Implementations;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Convertors;
using BackEnd.Core.utilities.Extensions.Connection;
using BackEnd.Core.utilities.Extensions.StartUpConfigurations;
using BackEnd.DataLayer.Context;
using BackEnd.DataLayer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text;

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

            //services.AddSwaggerConfiguration();
            #region Add DbContext
            services.AddApplicationDbContext(Configuration);
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion

            services.AddServicesConfiguration();

            services.AddAuthenticationConfiguration(Configuration);

            services.AddCorseConfiguration(Configuration);

            services.AddControllers();
            
            services.AddControllersWithViews();
            
            services.AddRazorPages();
            
            services.AddMvc();
            
            services.AddElmahConfiguration();
            
            services.AddResponseCompression(opt => opt.Providers.Add<GzipCompressionProvider>());
            #region Publish Section
           // services.AddSpaStaticFiles();
                
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {


                app.UseDeveloperExceptionPage();
            }

            //app.UseSwaggerConfiguration();

            app.UseCors(Configuration["Cors:PolicyString"]);

            app.UseAuthenticationConfiguration();

            app.UseElmahConfiguration();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSpaStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
            });

            app.UseEndpoints(end =>
            {
                end.MapControllers();
                end.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=home}/{action=index}");
            });
            #region Publish Section

            app.UseRewriter(new RewriteOptions().AddRedirect(@"^\s*$", "/app/", 301));

            app.Map("/app", site =>
            {
                site.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "wwwroot/app/";
                    spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/app"))
                    };

                });
            }).Map("/panel", panel =>
            {
                panel.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "wwwroot/panel/";
                    spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/panel"))
                    };
                });
            });

            #endregion

        }
    }
}
