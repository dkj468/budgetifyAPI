using budgetifyAPI.Dtos;
using budgetifyAPI.Enums;
using budgetifyAPI.Models;
using budgetifyAPI.Repository.Accounts;
using budgetifyAPI.Repository.Expenses;
using budgetifyAPI.Repository.Users;

namespace budgetifyAPI.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<ExpenseService> _logger;
        public ExpenseService(IUnitOfWork unitOfWork, ILogger<ExpenseService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
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

        public Task<ExpenseCategoryDto> CreateExpenseCategory(CreateExpenseCategoryDto createExpenseCategory)
        {
            throw new NotImplementedException();
        }

        public Task<ExpenseTypesDto> CreateExpenseType(CreateExpenseTypeDto createExpenseType)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ExpenseCategoryDto>> GetAllExpenseCategories()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ExpenseDto>> GetAllExpenses()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<ExpenseTypesDto>> GetAllExpenseTypes()
        {
            throw new NotImplementedException();
        }
    }
}
