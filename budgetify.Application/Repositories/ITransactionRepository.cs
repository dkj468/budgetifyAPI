using Budgetify.Domain.Entities;

namespace budgetify.Application.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<AccountTransaction>> GetAllTransactions();
        Task CreateNewTransaction(AccountTransaction accountTransaction, bool IsSave = true);

    }
}
