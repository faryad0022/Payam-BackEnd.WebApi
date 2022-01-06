using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.utilities.Extensions.StartUpConfigurations
{
    public static class ElmahConfiguration
    {
        public static void AddElmahService(this IServiceCollection services)
        {
            services.AddElmahIo(o =>
            {
                o.ApiKey = "11daace02f3d4a348002f48d8723bda8";
                o.LogId = new Guid("f23c2df8-a961-4a50-a75c-92e208dd0369");
            });
        }

        public static void UseElmah(this IApplicationBuilder app)
        {
            app.UseElmahIo();

        }
    }
}
