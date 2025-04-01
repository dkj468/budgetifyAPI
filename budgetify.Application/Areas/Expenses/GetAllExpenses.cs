using budgetify.Application.Dtos;
using budgetify.Application.Repositories;
using MediatR;

namespace budgetify.Application.Areas.Expenses
{
    public class GetAllExpenses
    {
        public class Query : IRequest<List<ExpenseDto>> { }

        public class Handler : IRequestHandler<Query, List<ExpenseDto>>
        {
            private readonly IExpenseRepository _expenseRepo;
            public Handler(IExpenseRepository expenseRepository)
            {
                _expenseRepo = expenseRepository;
            }
            public async Task<List<ExpenseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var data = await _expenseRepo.GetAllExpenses();
                var expenses = new List<ExpenseDto>();
                foreach (var expense in data)
                {
                    var thisexpense = new ExpenseDto();
                    thisexpense.Id = expense.Id;
                    thisexpense.DateCreated = expense.DateCreated;
                    thisexpense.DateUpdated = expense.DateCreated;
                    thisexpense.ExpenseCategoryId = expense.ExpenseCategoryId;
                    thisexpense.ExpenseTypeId = expense.ExpenseTypeId;
                    thisexpense.ExpenseType = expense.ExpenseType.Name;
                    thisexpense.ExpenseCategory = expense.ExpenseCategory.Name;
                    thisexpense.Amount = expense.Amount;
                    thisexpense.Description = expense.Description;
                    thisexpense.AccountName = expense.Account.Name;
                    thisexpense.AccountId = expense.AccountId;
                    expenses.Add(thisexpense);
                }
                return expenses;
            }
        }
    }
}
