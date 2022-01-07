using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace BackEnd.Core.utilities.Extensions.StartUpConfigurations
{
    public static class SpaConfiguration
    {
        public static void AddSiteSpa(this IServiceCollection services)
        {
            services.AddSpaStaticFiles();
        }
        public static void UseSiteSpaConfiguration(this IApplicationBuilder app, IFileProvider fileProvider)
        {
            #region Publish Section
            app.UseSpaStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
            });

            app.UseRewriter(new RewriteOptions().AddRedirect(@"^\s*$", "/app/", 301));

            app.Map("/app", site =>
            {
                site.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "wwwroot/app/";
                    spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                    {
                        FileProvider = fileProvider
                    };

                });
            }).Map("/panel", panel =>
            {
                panel.UseSpa(spa =>
                {
                    spa.Options.SourcePath = "wwwroot/panel/";
                    spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                    {
                        FileProvider = fileProvider
                    };
                });
            });

            #endregion

        }
    }
}
