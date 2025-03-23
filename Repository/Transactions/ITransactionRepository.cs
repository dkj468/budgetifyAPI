using budgetifyAPI.Dtos;
using budgetifyAPI.Models;

namespace budgetifyAPI.Repository.Transactions
{
    public interface ITransactionRepository
    {
        Task<ICollection<TransactionDto>> GetAllTransactions();
        Task CreateNewTransaction(AccountTransaction accountTransaction, bool IsSave = true);

    }
}
