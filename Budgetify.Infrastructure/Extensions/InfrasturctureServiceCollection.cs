
using budgetify.Application.Services;
using Budgetify.Infrastructure;
using Budgetify.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace budgetify.Infrastructure.Extensions
{
    public static class InfrasturctureServiceCollection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string jwtTokenKey)
        {
            services.AddScoped<ITokenService>(provider => new TokenService (jwtTokenKey));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailBackgroundService, EmailBackgroundService>();

            return services;
        }
    }
}
