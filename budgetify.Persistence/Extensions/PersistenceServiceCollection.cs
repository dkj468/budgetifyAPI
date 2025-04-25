
using budgetify.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace budgetify.Persistence.Extensions
{
    public static class PersistenceServiceCollection
    {
        public static IServiceCollection AddPersistenceServices (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["dbconnection"];
            services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            return services;
        }
    }
}
