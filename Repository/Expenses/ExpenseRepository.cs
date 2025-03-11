using budgetifyAPI.Data;
using budgetifyAPI.Dtos;
using budgetifyAPI.Enums;
using budgetifyAPI.Models;
using budgetifyAPI.Repository.Accounts;
using budgetifyAPI.Repository.Users;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace budgetifyAPI.Repository.Expenses
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly DataContext _ctx;
        private readonly IAccountRepository _accountRepo;
        private readonly IUserRepository _userRepo;
        public ExpenseRepository(DataContext ctx, IAccountRepository accountRepo
            , IUserRepository userRepo)
        {
            _ctx = ctx;
            _accountRepo = accountRepo;
            _userRepo = userRepo;
        }

        private AccountTransaction CreateTransaction(Account account, Expense expense)
        {
            var ThisTransaction = new AccountTransaction
            {
                Type = TransactionType.Expense,
                AccountId = expense.AccountId,
                Amount = expense.Amount,
                Description = expense.Description,
                ClosingBalance = account.Balance - expense.Amount,
                UserId = _userRepo.User.Id
            };

            return ThisTransaction;
        }

        public async Task<ExpenseDto> CreateExpense(CreateExpenseDto expense)
        {
            var account = await _ctx.Accounts.FindAsync(expense.AccountId);
            if (account == null)
            {
                throw new BadHttpRequestException($"No account found with given id: {expense.AccountId}");
            }
            var expenseCategory = await _ctx.ExpenseCategories.FindAsync(expense.ExpenseCategoryId);
            if (expenseCategory == null)
            {
                throw new BadHttpRequestException($"No expense category found with given id: {expense.ExpenseCategoryId}");
            }
            var expenseType = await _ctx.ExpenseTypes.FindAsync(expense.ExpenseTypeId);
            if (expenseType == null)
            {
                throw new BadHttpRequestException($"No expense type found with given id: {expense.ExpenseTypeId}");
            }
            var newExpense = new Expense
            {
                ExpenseCategoryId = expense.ExpenseCategoryId,
                Amount = expense.Amount,
                AccountId = expense.AccountId,
                Description = expense.Description,
                ExpenseTypeId = expense.ExpenseTypeId,
                UserId = _userRepo.User.Id,


            };
            var ThisTransaction = CreateTransaction(account, newExpense);
            _ctx.AccountTransactions.Add(ThisTransaction);
            // update account balance --- account is being tracked in context
            account.Balance = account.Balance - expense.Amount;
            await _ctx.SaveChangesAsync();

            newExpense.Transaction = ThisTransaction;
            _ctx.Expenses.Add(newExpense);
            await _ctx.SaveChangesAsync();

            return new ExpenseDto
            {
                Id = newExpense.Id,
                DateCreated = newExpense.DateCreated,
                DateUpdated = newExpense.DateUpdated,
                ExpenseCategoryId = newExpense.ExpenseCategoryId,
                ExpenseTypeId = newExpense.ExpenseTypeId,
                Amount = newExpense.Amount,
                Description = newExpense.Description,
                AccountName = newExpense.Account.Name,
                AccountId = newExpense.AccountId,
                Transaction = new TransactionDto
                {
                    Id = ThisTransaction.Id,
                    AccountId = account.Id,
                    AccountName = account.Name,
                    Amount = expense.Amount,
                    ClosingBalance = ThisTransaction.ClosingBalance,
                    Description = ThisTransaction.Description,
                    Type = TransactionType.Expense.ToString(),
                    DateCreated = ThisTransaction.DateCreated,
                    DateUpdated = ThisTransaction.DateUpdated,
                }
            };

        }

        public async Task<ICollection<ExpenseDto>> GetAllExpenses()
        {
            var expenses = await _ctx.Expenses
                .Include(e => e.ExpenseType)
                .Include(e => e.ExpenseCategory)
                .Include(e => e.Account)
                .Include(e => e.Transaction)
                .Where(e => e.UserId == _userRepo.User.Id)
                .ToListAsync();
            var expensesDtoList = new List<ExpenseDto>();
            foreach (var expense in expenses)
            {
                var thisexpense = new ExpenseDto();
                thisexpense.Id = expense.Id;
                thisexpense.DateCreated = expense.DateCreated;
                thisexpense.DateCreated = expense.DateCreated;
                thisexpense.ExpenseCategoryId = expense.ExpenseCategoryId;
                thisexpense.ExpenseTypeId = expense.ExpenseTypeId;
                thisexpense.ExpenseType = expense.ExpenseType.Name;
                thisexpense.ExpenseCategory = expense.ExpenseCategory.Name;
                thisexpense.Amount = expense.Amount;
                thisexpense.Description = expense.Description;
                thisexpense.AccountName = expense.Account.Name;
                thisexpense.AccountId = expense.AccountId;                
                expensesDtoList.Add(thisexpense);
            }
            return expensesDtoList;
        }

        public async Task<ICollection<ExpenseTypesDto>> GetAllExpenseTypes()
        {
            var data = await _ctx.ExpenseTypes
                            .Include(et => et.ExpenseCategories)
                            .Where(et => et.UserId == _userRepo.User.Id)
                            .ToListAsync();
            var expenseTypes = new List<ExpenseTypesDto>();
            foreach (var expenseType in data)
            {
                var expenseTypeDto = new ExpenseTypesDto();
                expenseTypeDto.Id = expenseType.Id;
                expenseTypeDto.Name = expenseType.Name;
                expenseTypeDto.Description = expenseType.Description;
                expenseTypeDto.ExpenseCategories = new List<ExpenseCategoryDto>();
                foreach (var category in expenseType.ExpenseCategories)
                {
                    var expenseCategoryDto = new ExpenseCategoryDto();
                    expenseCategoryDto.Id = category.Id;
                    expenseCategoryDto.Name = category.Name;
                    expenseCategoryDto.Description = category.Description;
                    expenseCategoryDto.ExpenseTypeDescription = expenseType.Description;
                    expenseCategoryDto.ExpenseTypeName = expenseType.Name;
                    expenseCategoryDto.ExpenseTypeId = expenseType.Id;


                    expenseTypeDto.ExpenseCategories.Add(expenseCategoryDto);
                }
                expenseTypes.Add(expenseTypeDto);
            }

            return expenseTypes;
        }

        public async Task<ICollection<ExpenseCategoryDto>> GetAllExpenseCategories()
        {
            var data = await _ctx.ExpenseCategories
                            .Include(ec => ec.ExpenseType)
                            .Where(ec => ec.UserId == _userRepo.User.Id)
                            .ToListAsync();
            var expenseCategories = new List<ExpenseCategoryDto>();
            foreach (var expenseCategory in data)
            {
                var expenseCategoryDto = new ExpenseCategoryDto();
                expenseCategoryDto.Id = expenseCategory.Id;
                expenseCategoryDto.Name = expenseCategory.Name;
                expenseCategoryDto.Description = expenseCategory.Description;
                expenseCategoryDto.ExpenseTypeDescription = expenseCategory.ExpenseType.Description;
                expenseCategoryDto.ExpenseTypeName = expenseCategory.ExpenseType.Name;
                expenseCategoryDto.ExpenseTypeId = expenseCategory.ExpenseType.Id;
                expenseCategories.Add(expenseCategoryDto);
            }
            return expenseCategories;
        }

        public async Task<ExpenseTypesDto> CreateExpenseType(CreateExpenseTypeDto createExpenseType)
        {
            var newExpenseType = new ExpenseType
            {
                Name = createExpenseType.Name,
                Description = createExpenseType.Description,
                AddedBy = AddedBy.User,
                UserId = _userRepo.User.Id,
            };

            _ctx.ExpenseTypes.Add(newExpenseType);
            await _ctx.SaveChangesAsync();

            return new ExpenseTypesDto
            {
                Id = newExpenseType.Id,
                Name = newExpenseType.Name,
                Description = newExpenseType.Description
            };
        }

        public async Task<ExpenseCategoryDto> CreateExpenseCategory(CreateExpenseCategoryDto createExpenseCategory)
        {
            var expenseType = await _ctx.ExpenseTypes.FindAsync(createExpenseCategory.ExpenseTypeId);
            if (expenseType == null)
            {
                throw new BadHttpRequestException($"No expense type found with given id: {createExpenseCategory.ExpenseTypeId}");
            }
            var newExpenseCategory = new ExpenseCategory
            {
                Name = createExpenseCategory.Name,
                Description = createExpenseCategory.Description,
                ExpenseTypeId = createExpenseCategory.ExpenseTypeId,
                AddedBy = AddedBy.User,
                UserId = _userRepo.User.Id,
                ExpenseType = expenseType
            };
            _ctx.ExpenseCategories.Add(newExpenseCategory);
            await _ctx.SaveChangesAsync();

            return new ExpenseCategoryDto
            {
                Id= newExpenseCategory.Id,
                Name = newExpenseCategory.Name,
                Description = newExpenseCategory.Description,
                ExpenseTypeId = newExpenseCategory.ExpenseTypeId,
                ExpenseTypeDescription = newExpenseCategory.ExpenseType.Description,
                ExpenseTypeName = newExpenseCategory.ExpenseType.Name
            };
        }
    }
}
