using budgetify.Application.Dtos;
using budgetify.Application.Repositories;
using MediatR;

namespace budgetify.Application.Areas.Expenses
{
    public class GetAllExpenseType
    {
        public class Query : IRequest<List<ExpenseTypesDto>> { }

        public class Handler : IRequestHandler<Query, List<ExpenseTypesDto>>
        {
            private readonly IExpenseRepository _expenseRepo;
            public Handler(IExpenseRepository expenseRepository)
            {
                _expenseRepo = expenseRepository;
            }
            public async Task<List<ExpenseTypesDto>> Handle(Query request, CancellationToken cancellationToken)
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
}
