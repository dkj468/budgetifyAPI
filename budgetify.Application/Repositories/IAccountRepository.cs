using Budgetify.Domain.Entities;
using Budgetify.Domain.Enums;

namespace budgetify.Application.Repositories
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAccounts();
        Task<Account> GetAccountById(int accountId);
        Task CreateAccount(Account account);
        Task UpdateAccountBalance(int accountId, decimal amount, TransactionType type);
    }
}
