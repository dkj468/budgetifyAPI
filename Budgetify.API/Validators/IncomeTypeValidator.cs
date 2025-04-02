using budgetify.Application.Dtos;
using FluentValidation;

namespace budgetifyAPI.Validators
{
    public class IncomeTypeValidator : AbstractValidator<CreateIncomeTypeDto>
    {
        public IncomeTypeValidator() 
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Income type name cannot be empty");
        }
    }
}
