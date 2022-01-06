using BackEnd.Core.Security;
using BackEnd.Core.Services.Implementations;
using BackEnd.Core.Services.Interfaces;
using BackEnd.Core.utilities.Convertors;
using Microsoft.Extensions.DependencyInjection;


namespace BackEnd.Core.utilities.Extensions.StartUpConfigurations
{
    public static class DiCongiguration
    {
        public static void ServicesInjection(this IServiceCollection services)
        {
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
        }
    }
}
