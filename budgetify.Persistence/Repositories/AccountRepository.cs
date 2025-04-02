using budgetify.Application.Repositories;
using budgetify.Persistence.Contexts;
using Budgetify.Domain.Entities;
using Budgetify.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace budgetify.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _ctx;
        private readonly IUserRepository _userRepository;
        public AccountRepository(DataContext ctx, IUserRepository userRepository)
        {
            _ctx = ctx;
            _userRepository = userRepository;
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            var data = await _ctx.Accounts
                                .Where(a => a.UserId == _userRepository.User.Id)
                                .ToListAsync();
            return data;
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

        public async Task CreateAccount(Account account)
        {            
            _ctx.Accounts.Add(account);
            await _ctx.SaveChangesAsync();
        }
    }
}
