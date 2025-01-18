using budgetifyAPI.Data;
using budgetifyAPI.Dtos;
using budgetifyAPI.Enums;
using budgetifyAPI.Models;
using budgetifyAPI.Repository.Accounts;
using budgetifyAPI.Repository.Users;
using Microsoft.EntityFrameworkCore;

namespace budgetifyAPI.Repository.Incomes
{
    public class IncomeRepostory : IIncomeRepository
    {
        private readonly DataContext _ctx;
        private readonly IAccountRepository _accountRepo;
        private readonly IUserRepository _userRepo;
        public IncomeRepostory(DataContext ctx, IAccountRepository accountRepo, IUserRepository userRepo)
        {
            _ctx = ctx;
            _accountRepo = accountRepo;
            _userRepo = userRepo;
        }

        private AccountTransaction CreateTransaction(Account account, Income income)
        {

            var ThisTransaction = new AccountTransaction
            {
                Type = TransactionType.Income,
                AccountId = income.AccountId,
                Amount = income.Amount,
                Description = income.Description,
                ClosingBalance = account.Balance + income.Amount,
            };

            return ThisTransaction;
        }
        public async Task CreateIncome(CreateIncomeDto income)
        {
            var account = await _ctx.Accounts.FindAsync(income.AccountId);
            var ThisIncome = new Income
            {
                IncomeTypeId = income.IncomeTypeId,
                Amount = income.Amount,
                AccountId = income.AccountId,
                Description = income.Description,

            };
            _ctx.Incomes.Add(ThisIncome);
            var ThisTransaction = CreateTransaction(account, ThisIncome);
            _ctx.AccountTransactions.Add(ThisTransaction);
            // update account balance --- account is being tracked in context
            account.Balance = account.Balance + income.Amount;
            await _ctx.SaveChangesAsync();

        }

        public async Task<ICollection<IncomeType>> GetAllIncomeType()
        {
            return await _ctx.IncomeTypes.ToListAsync();
        }

        public async Task<IncomeType> CreateIncomeType(CreateIncomeTypeDto createIncomeType)
        {
            var newIncomeType = new IncomeType
            {
                Name = createIncomeType.Name,
                Description = createIncomeType.Description,
                AddedBy = AddedBy.User,
                UserId = _userRepo.User.Id,
            };
            _ctx.IncomeTypes.Add(newIncomeType);
            await _ctx.SaveChangesAsync();

            return new IncomeType 
                {
                    Id = newIncomeType.Id,
                    Name = newIncomeType.Name, 
                    Description = newIncomeType.Description, 
                };
        }
    }
}
