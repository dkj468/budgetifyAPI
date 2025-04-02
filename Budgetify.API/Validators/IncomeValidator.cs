using budgetify.Application.Dtos;
using FluentValidation;

namespace budgetifyAPI.Validators
{
    public class IncomeValidator : AbstractValidator<CreateIncomeDto>
    {
        public IncomeValidator() 
        {
            RuleFor(x => x.AccountId).NotEqual(-1).WithMessage("Account Id cannot be empty");
            RuleFor(x => x.IncomeTypeId).NotEqual(-1).WithMessage("Income type id cannot be empty");
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("The income value should be greater than zero");
        }
    }
}
