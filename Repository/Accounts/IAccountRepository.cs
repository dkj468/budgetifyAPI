using budgetifyAPI.Dtos;
using budgetifyAPI.Enums;
using budgetifyAPI.Models;

namespace budgetifyAPI.Repository.Accounts
{
    public interface IAccountRepository
    {
        Task<ICollection<AccountDto>> GetAllAccounts();
        Task<Account> GetAccountById(int accountId);
        Task UpdateAccountBalance(int accountId, decimal amount, TransactionType type);
    }
}
