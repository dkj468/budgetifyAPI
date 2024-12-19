using budgetifyAPI.Dtos;

namespace budgetifyAPI.Repository
{
    public interface ITransactionRepository
    {
        Task<ICollection<TransactionDto>> GetAllTransactions();
        
    }
}
