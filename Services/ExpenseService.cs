using budgetifyAPI.Dtos;
using budgetifyAPI.Enums;
using budgetifyAPI.Models;
using budgetifyAPI.Repository.Accounts;
using budgetifyAPI.Repository.Expenses;
using budgetifyAPI.Repository.Users;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace budgetifyAPI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<ExpenseService> _logger;
        private readonly IExpenseRepository _expenseRepo;
        private readonly IUserRepository _userRepo;
        public ExpenseService(IUnitOfWork unitOfWork, ILogger<ExpenseService> logger, IExpenseRepository expenseRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _expenseRepo = expenseRepository;
            _userRepo = userRepository;
        }

        public async Task<ICollection<ExpenseDto>> GetAllExpenses()
        {
            var expenses = await _expenseRepo.GetAllExpenses();
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

        private AccountTransaction CreateTransaction(Account account, Expense expense)
        {
            var ThisTransaction = new AccountTransaction
            {
                Type = TransactionType.Expense,
                AccountId = expense.AccountId,
                Amount = expense.Amount,
                Description = expense.Description,
                ClosingBalance = account.Balance - expense.Amount,
                UserId = _unitOfWork.userRepo.User.Id
            };

            return ThisTransaction;
        }
        public async Task<ExpenseDto?> CreateExpense(CreateExpenseDto expense)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var account = await _unitOfWork.accountRepo.GetAccountById(expense.AccountId);
                if (account == null)
                {
                    throw new BadHttpRequestException($"No account found with given id: {expense.AccountId}");
                }
                var expenseCategory = await _unitOfWork.expenseRepo.GetExpenseCategoryById(expense.ExpenseCategoryId);
                if (expenseCategory == null)
                {
                    throw new BadHttpRequestException($"No expense category found with given id: {expense.ExpenseCategoryId}");
                }
                var expenseType = await _unitOfWork.expenseRepo.GetExpenseTypeById(expense.ExpenseTypeId);
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
                    UserId = _unitOfWork.userRepo.User.Id,


                };
                var ThisTransaction = CreateTransaction(account, newExpense);
                await _unitOfWork.transactionRepo.CreateNewTransaction(ThisTransaction, false);
                // update account balance --- account is being tracked in context
                account.Balance = account.Balance - expense.Amount;

                newExpense.Transaction = ThisTransaction;
                await _unitOfWork.expenseRepo.CreateExpense(newExpense, false);
                await _unitOfWork.SaveChangesAsync();

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
            catch (Exception ex)
            {
                _logger.LogError($"Exception creating a new expense : {ex.Message}");
            }
            return null;
        }

        public async Task<ExpenseCategoryDto> CreateExpenseCategory (CreateExpenseCategoryDto expenseCategory)
        {
            var expenseType = await _expenseRepo.GetExpenseTypeById (expenseCategory.ExpenseTypeId);
            if (expenseType == null)
            {
                throw new BadHttpRequestException($"No expense type found with given id: {expenseCategory.ExpenseTypeId}");
            }

            var newExpenseCategory = new ExpenseCategory
            {
                Name = expenseCategory.Name,
                Description = expenseCategory.Description,
                ExpenseTypeId = expenseCategory.ExpenseTypeId,
                AddedBy = AddedBy.User,
                UserId = _userRepo.User.Id,
                ExpenseType = expenseType
            };

            await _expenseRepo.CreateExpenseCategory(newExpenseCategory);

            return new ExpenseCategoryDto
            {
                Id = newExpenseCategory.Id,
                Name = newExpenseCategory.Name,
                Description = newExpenseCategory.Description,
                ExpenseTypeId = newExpenseCategory.ExpenseTypeId,
                ExpenseTypeDescription = newExpenseCategory.ExpenseType.Description,
                ExpenseTypeName = newExpenseCategory.ExpenseType.Name
            };
        }

        public async Task<ExpenseTypesDto> CreateExpenseType (CreateExpenseTypeDto expenseTypeDto)
        {
            var newExpenseType = new ExpenseType
            {
                Name = expenseTypeDto.Name,
                Description = expenseTypeDto.Description,
                AddedBy = AddedBy.User,
                UserId = _userRepo.User.Id,
            };

            await _expenseRepo.CreateExpenseType(newExpenseType);

            return new ExpenseTypesDto
            {
                Id = newExpenseType.Id,
                Name = newExpenseType.Name,
                Description = newExpenseType.Description
            };
        }

        public async Task<ICollection<ExpenseCategoryDto>> GetAllExpenseCategories()
        {
            var data = await _expenseRepo.GetAllExpenseCategories();
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

        public async Task<ICollection<ExpenseTypesDto>> GetAllExpenseTypes()
        {
            var data = await _expenseRepo.GetAllExpenseTypes();
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
    }
}
