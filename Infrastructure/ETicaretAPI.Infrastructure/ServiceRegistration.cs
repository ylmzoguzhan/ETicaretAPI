using ETicaretAPI.Application.Services;
using ETicaretAPI.Infrastructure.Services;
using ETicaretAPI.Persistence.Repositories.Customer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureervices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();

        }
    }
}
