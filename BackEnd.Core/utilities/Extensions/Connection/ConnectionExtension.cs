using BackEnd.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace BackEnd.Core.utilities.Extensions.Connection
{
    public static class ConnectionExtension
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection service, IConfiguration configuration) {
            service.AddDbContext<BackEndDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Production"));

            });
            return service;
        }
    }
}
