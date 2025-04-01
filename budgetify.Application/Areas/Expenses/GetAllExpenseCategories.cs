using budgetify.Application.Dtos;
using budgetify.Application.Repositories;
using MediatR;

namespace budgetify.Application.Areas.Expenses
{
    public class GetAllExpenseCategories
    {
        public class Query : IRequest<List<ExpenseCategoryDto>> { }

        public class Handler : IRequestHandler<Query, List<ExpenseCategoryDto>>
        {
            private readonly IExpenseRepository _expenseRepo;
            public Handler(IExpenseRepository expenseRepository)
            {
                _expenseRepo = expenseRepository;
            }
            public async Task<List<ExpenseCategoryDto>> Handle(Query request, CancellationToken cancellationToken)
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
        }
    }
}
