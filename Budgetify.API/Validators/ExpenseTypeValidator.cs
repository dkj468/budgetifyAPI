using budgetify.Application.Dtos;
using FluentValidation;

namespace budgetifyAPI.Validators
{
    public class ExpenseTypeValidator : AbstractValidator<CreateExpenseTypeDto>
    {
        public ExpenseTypeValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Expense type name cannot be empty");
        }
    }
}
