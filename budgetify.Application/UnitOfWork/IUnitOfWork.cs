using budgetify.Application.Repositories;

namespace budgetifyAPI.Services
{
    public interface IUnitOfWork
    {
        IExpenseRepository expenseRepo { get; }
        IIncomeRepository incomeRepo { get; }
        IAccountRepository accountRepo { get; }
        IUserRepository userRepo { get; }
        ITransactionRepository transactionRepo { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

    }
}
