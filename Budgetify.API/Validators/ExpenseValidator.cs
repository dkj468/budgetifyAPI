using budgetify.Application.Dtos;
using FluentValidation;

namespace budgetifyAPI.Validators
{
    public class ExpenseValidator : AbstractValidator<CreateExpenseDto>
    {
        public ExpenseValidator()
        {
            RuleFor(x => x.AccountId).NotEqual(-1).WithMessage("Account Id cannot be empty");
            RuleFor(x => x.ExpenseCategoryId).NotEqual(-1).WithMessage("Expense category Id cannot be empty");
            RuleFor(x => x.ExpenseTypeId).NotEqual(-1).WithMessage("Expense type Id cannot be empty");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("The expense amount should be greater than zero");
        }
    }
}
