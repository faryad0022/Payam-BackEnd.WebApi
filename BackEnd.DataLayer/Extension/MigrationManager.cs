using BackEnd.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.DataLayer.Extension
{
    public static class MigrationManager
    {
        //زمانی که برنامه اجرا میشه migration اجرا میشه
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using var appContext = scope.ServiceProvider.GetRequiredService<BackEndDbContext>();
                try
                {
                    appContext.Database.Migrate();
                }
                catch (Exception)
                {
                    //Log errors or do anything you think it's needed
                    throw;
                }
            }
            return host;
        }
    }
}
