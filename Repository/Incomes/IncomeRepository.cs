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
        public IncomeRepostory(DataContext ctx, IAccountRepository accountRepo)
        {
            _ctx = ctx;
            _accountRepo = accountRepo;
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
    }
}
