using budgetify.Application.Dtos;
using FluentValidation;

namespace budgetifyAPI.Validators
{
    public class ExpenseCategoryValidator : AbstractValidator<CreateExpenseCategoryDto>
    {
        public ExpenseCategoryValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Expense category name cannot be empty");
            RuleFor(x => x.ExpenseTypeId).NotEqual(-1).WithMessage("Expense type id cannot be empty");
        }
    }
}
