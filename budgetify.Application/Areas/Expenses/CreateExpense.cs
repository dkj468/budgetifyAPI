using budgetify.Application.Dtos;
using budgetify.Application.Helpers;
using Budgetify.Domain.Commons.Exceptions;
using Budgetify.Domain.Entities;
using Budgetify.Domain.Enums;
using budgetifyAPI.Services;
using MediatR;

namespace budgetify.Application.Areas.Expenses
{
    public class CreateExpense
    {
        public class Request : IRequest<ExpenseDto>
        {
            public CreateExpenseDto expense;
        }

        public class Handler : IRequestHandler<Request, ExpenseDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            public Handler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<ExpenseDto> Handle(Request request, CancellationToken cancellationToken)
            {
                await _unitOfWork.BeginTransactionAsync();
                var expenseValidationResult = await _unitOfWork.expenseRepo.ValidateExpense(request.expense);
                if (!expenseValidationResult.IsAccountExists)
                {
                    throw new NotFoundException("account", request.expense.AccountId);
                }
                if (!expenseValidationResult.IsExpenseTypeExists)
                {
                    throw new NotFoundException("Expense Type", request.expense.ExpenseTypeId);
                }

                if (!expenseValidationResult.IsExpenseCategoryExists)
                {
                    throw new NotFoundException("Expense Category", request.expense.ExpenseCategoryId);
                }

                var account = await _unitOfWork.accountRepo.GetAccountById(request.expense.AccountId);
                var newExpense = new Expense
                {
                    ExpenseCategoryId = request.expense.ExpenseCategoryId,
                    Amount = request.expense.Amount,
                    AccountId = request.expense.AccountId,
                    Description = request.expense.Description,
                    ExpenseTypeId = request.expense.ExpenseTypeId,
                    UserId = _unitOfWork.userRepo.User.Id,


                };
                var ThisTransaction = TransactionHelper.CreateTransaction(TransactionType.Expense, account, request.expense.Amount, request.expense.Description, _unitOfWork.userRepo.User.Id);
                await _unitOfWork.transactionRepo.CreateNewTransaction(ThisTransaction, false);
                // update account balance --- account is being tracked in context
                account.Balance = account.Balance - request.expense.Amount;

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
                        Amount = request.expense.Amount,
                        ClosingBalance = ThisTransaction.ClosingBalance,
                        Description = ThisTransaction.Description,
                        Type = TransactionType.Expense.ToString(),
                        DateCreated = ThisTransaction.DateCreated,
                        DateUpdated = ThisTransaction.DateUpdated,
                    }
                };
            }
        }
    }
}
