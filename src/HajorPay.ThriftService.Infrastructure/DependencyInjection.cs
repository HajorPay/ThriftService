using HajorPay.ThriftService.Application.Abstractions.Identity;
using HajorPay.ThriftService.Application.Data.Interfaces;
using HajorPay.ThriftService.Infrastructure.Contexts;
using HajorPay.ThriftService.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HajorPay.ThriftService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IHajorPayDbContext, ApplicationDbContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
