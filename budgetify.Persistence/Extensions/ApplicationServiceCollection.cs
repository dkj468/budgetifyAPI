using budgetify.Application.Repositories;
using budgetify.Application.Services;
using budgetify.Persistence.Repositories;
using budgetifyAPI.Repository.Incomes;
using budgetifyAPI.Repository.Transactions;
using budgetifyAPI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace budgetify.Persistence.Extensions
{
    public static class ApplicationServiceCollection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepostory>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
