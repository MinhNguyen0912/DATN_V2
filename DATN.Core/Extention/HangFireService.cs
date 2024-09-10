using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Extention
{
    public static class HangFireService
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(config =>
                config.UseSqlServerStorage("DATNDbContextConnection")); // Cấu hình kết nối SQL Server
            services.AddHangfireServer();
        }
    }
}
