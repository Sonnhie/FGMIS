using FGScanner.Services.Classes;
using FGScanner.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGScanner.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
           // services.AddScoped<PrintService>();
           // services.AddScoped<TransactionService>();
          //  services.AddScoped<ExcelService>();


            services.AddScoped<IAuthInterface, AuthenticationServices>();

            return services;
        }
    }
}
