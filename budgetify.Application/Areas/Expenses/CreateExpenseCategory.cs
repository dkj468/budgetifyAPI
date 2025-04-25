using budgetify.Application.Dtos;
using budgetify.Application.Helpers;
using budgetify.Application.Repositories;
using Budgetify.Domain.Commons.Exceptions;
using Budgetify.Domain.Entities;
using Budgetify.Domain.Enums;
using MediatR;

namespace budgetify.Application.Areas.Expenses
{
    public class CreateExpenseCategory
    {
        public class Command : IRequest<ExpenseCategoryDto>
        {
            public CreateExpenseCategoryDto expenseCategory;
        }

        public class Handler : IRequestHandler<Command, ExpenseCategoryDto>
        {
            private readonly IExpenseRepository _expenseRepo;
            private readonly IUserRepository _userRepo;

            public Handler(IExpenseRepository expenseRepository, IUserRepository userRepository)
            {
                _expenseRepo = expenseRepository;
                _userRepo = userRepository;
            }
            public async Task<ExpenseCategoryDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var expenseType = await _expenseRepo.GetExpenseTypeById(request.expenseCategory.ExpenseTypeId);
                if (expenseType == null)
                {
                    throw new NotFoundException("Expense Type", request.expenseCategory.ExpenseTypeId);
                }

                var newExpenseCategory = new ExpenseCategory
                {
                    Name = request.expenseCategory.Name,
                    Description = request.expenseCategory.Description,
                    ExpenseTypeId = request.expenseCategory.ExpenseTypeId,
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
        }
    }
}
