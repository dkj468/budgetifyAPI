using budgetifyAPI.Data;
using budgetifyAPI.Dtos;
using budgetifyAPI.Enums;
using budgetifyAPI.Models;
using budgetifyAPI.Repository.Users;
using Microsoft.EntityFrameworkCore;

namespace budgetifyAPI.Repository.Accounts
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

        public async Task<AccountDto> CreateAccount(CreateAccountDto account)
        {
            var newAccount = new Account
            {
                Name = account.Name,
                Description = account.Description,
                Balance = account.Balance,
                ImageUrl = account.ImageUrl,
                AddedBy = AddedBy.User,
                UserId = _userRepository.User.Id
            };
            _ctx.Accounts.Add(newAccount);
            await _ctx.SaveChangesAsync();

            var accountDto = new AccountDto
            {
                Id = newAccount.Id,
                Name = newAccount.Name,
                Description = newAccount.Description,
                ImageUrl = newAccount.ImageUrl,
                Balance = newAccount.Balance,

            };
            return accountDto;

        }
    }
}
