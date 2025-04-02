
using budgetify.Application.Services;
using Budgetify.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace budgetify.Infrastructure.Extensions
{
    public static class InfrasturctureServiceCollection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string jwtTokenKey)
        {
            services.AddScoped<ITokenService>(provider => new TokenService (jwtTokenKey));

            return services;
        }
    }
}
