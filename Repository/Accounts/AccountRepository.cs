using budgetifyAPI.Data;
using budgetifyAPI.Dtos;
using budgetifyAPI.Enums;
using budgetifyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace budgetifyAPI.Repository.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _ctx;
        public AccountRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<ICollection<AccountDto>> GetAllAccounts()
        {
            var data = await _ctx.Accounts.ToListAsync();
            var accounts = new List<AccountDto>();
            foreach (var account in data)
            {
                var accountDto = new AccountDto();
                accountDto.Id = account.Id;
                accountDto.Name = account.Name;
                accountDto.Description = account.Description;
                accountDto.Balance = account.Balance;
                accountDto.ImageUrl = account.ImageUrl;
                accounts.Add(accountDto);
            }
            return accounts;

        }

        public async Task<Account> GetAccountById(int accountId)
        {
            var account = await _ctx.Accounts.FindAsync(accountId);
            return account;
        }

        public async Task UpdateAccountBalance(int accountId, decimal amount, TransactionType type)
        {
            var account = await _ctx.Accounts.FindAsync(accountId);
            if (type == TransactionType.Income)
            {
                account.Balance += amount;
            }
            if (type == TransactionType.Expense)
            {
                account.Balance -= amount;
            }
            await _ctx.SaveChangesAsync();
        }
    }
}
