using Dotne5WebAPITemplate.DAL.ErrorModule;
using Dotne5WebAPITemplate.Services.ErrorModule;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotne5WebAPITemplate.API.Helpers
{
    public static class DependencyInjection
    {
        public static void DependencyInjectionSetUp(this IServiceCollection services)
        {
            // inject repositories
            services.AddTransient<IErrorRepository, ErrorRepository>();


            // inject services
            services.AddTransient<IErrorService, ErrorService>();
        }
    }
}
