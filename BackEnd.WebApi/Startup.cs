using BackEnd.Core.Security;
using BackEnd.Core.Services.Implementations;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Convertors;
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
            /*
            #region swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });
            });
            #endregion
            */
            #region Add DbContext
            services.AddDbContext<BackEndDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Production"));

            }); 
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            #endregion

            #region Application Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IPasswordHelper, PasswordHelper>();
            services.AddScoped<IMailSender, SendEmail>();
            services.AddScoped<IViewRenderService, RenderViewToString>();
            services.AddScoped<IImageGalleryService, ImageGaleryService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ISocialService, SocialService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<IBlogContentService, BlogContentService>();
            services.AddScoped<IBlogGroupService, BlogGroupService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<ILogoService, LogoService>();
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<ICountNotificationService, CountNotificationService>();
            services.AddScoped<IAccessService, AccessService>();

            #endregion

            #region Authentication Setting
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            #endregion

            #region CORS Setting
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
            #endregion


            services.AddControllers();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();
            services.AddElmahIo(o =>
            {
                o.ApiKey = "11daace02f3d4a348002f48d8723bda8";
                o.LogId = new Guid("f23c2df8-a961-4a50-a75c-92e208dd0369");
            });
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

            /*
            #region swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            #endregion
            */
            app.UseCors(Configuration["Cors:PolicyString"]);
            app.UseAuthentication();
            app.UseElmahIo();
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
