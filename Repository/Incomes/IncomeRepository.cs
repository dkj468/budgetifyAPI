using budgetifyAPI.Data;
using budgetifyAPI.Dtos;
using budgetifyAPI.Enums;
using budgetifyAPI.Exceptions;
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
                UserId = _userRepo.User.Id
            };

            return ThisTransaction;
        }
        public async Task<IncomeDto> CreateIncome(CreateIncomeDto income)
        {
            var account = await _ctx.Accounts.FindAsync(income.AccountId);
            if(account == null)
            {
                throw new BadHttpRequestException ($"No account found with given id: {income.AccountId}");
            }
            var incomeTypeId = await _ctx.Incomes.FindAsync(income.IncomeTypeId);
            if (incomeTypeId == null)
            {
                throw new BadHttpRequestException($"No income type found with given id: {income.IncomeTypeId}");
            }
            var ThisIncome = new Income
            {
                IncomeTypeId = income.IncomeTypeId,
                Amount = income.Amount,
                AccountId = income.AccountId,
                Description = income.Description,
                UserId = _userRepo.User.Id

            };
            var ThisTransaction = CreateTransaction(account, ThisIncome);
            _ctx.AccountTransactions.Add(ThisTransaction);
            // update account balance --- account is being tracked in context
            account.Balance = account.Balance + income.Amount;
            ThisIncome.Transaction = ThisTransaction;

            _ctx.Incomes.Add(ThisIncome);
            await _ctx.SaveChangesAsync();

            return new IncomeDto
            {
                Id = ThisIncome.Id,
                IncomeTypeId = ThisIncome.IncomeTypeId,
                Amount = ThisIncome.Amount,
                AccountId = account.Id,
                Account = account.Description,
                DateCreated = ThisIncome.DateCreated,
                DateUpdated = ThisIncome.DateUpdated,
                Description = ThisIncome.Description,
                UserId = _userRepo.User.Id,
                Transaction = new TransactionDto
                {
                    Id = ThisTransaction.Id,
                    AccountId = account.Id,
                    AccountName = account.Name,
                    Amount = ThisIncome.Amount,
                    ClosingBalance = ThisTransaction.ClosingBalance,
                    DateCreated = ThisTransaction.DateCreated,
                    DateUpdated = ThisTransaction.DateUpdated,
                    Description = ThisTransaction.Description,
                    Type = TransactionType.Income.ToString()
                }

            };

        }

        public async Task<ICollection<IncomeType>> GetAllIncomeType()
        {
            return await _ctx.IncomeTypes
                        .Where(it => it.UserId ==  _userRepo.User.Id)
                        .ToListAsync();
        }

        public async Task<IncomeType> CreateIncomeType(CreateIncomeTypeDto createIncomeType)
        {
            var newIncomeType = new IncomeType
            {
                Name = createIncomeType.Name,
                Description = createIncomeType.Description,
                AddedBy = AddedBy.User, // AddedBy enum
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
