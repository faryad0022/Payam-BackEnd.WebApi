using BackEnd.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.utilities.Extensions.Connection
{
    public static class ConnectionExtension
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection service, IConfiguration configuration) {
            service.AddDbContext<BackEndDbContext>(options =>
            {
                var connectionString = "ConnectionStrings:PayamNewConnection:Development";
                options.UseSqlServer(configuration[connectionString]);

            });
            return service;
        }
    }
}
