using budgetifyAPI.Dtos;

namespace budgetifyAPI.Repository.Transactions
{
    public interface ITransactionRepository
    {
        Task<ICollection<TransactionDto>> GetAllTransactions();

    }
}
