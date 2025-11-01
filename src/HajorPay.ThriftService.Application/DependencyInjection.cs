using HajorPay.ThriftService.Application.Interfaces;
using HajorPay.ThriftService.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HajorPay.ThriftService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            });

            services.AddScoped<IVirtualAccountService, VirtualAccountService>();
            return services;
        }
    }
}
