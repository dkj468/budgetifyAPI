using budgetify.Application.Areas.Incomes;
using budgetify.Application.Dtos;
using budgetifyAPI.Factories;
using budgetifyAPI.Services;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace budgetifyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidationFactory _validationFactory;
        public IncomeController(IValidationFactory validationFactory, IMediator mediator)
        {
            _mediator = mediator;
            _validationFactory = validationFactory;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome(CreateIncomeDto income)
        {
            var validator = _validationFactory.GetValidator<CreateIncomeDto>();
            ValidationResult result = validator.Validate(income);
            if(!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return ValidationProblem
                (
                    title: "Validation problem",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }
            var newIncome = await _mediator.Send (new CreateIncome.Command { income = income });
            return CreatedAtAction(nameof(CreateIncome), newIncome);
        }


        [HttpGet("incometypes")]
        public async Task<IActionResult> GetAllIncomeTypes()
        {
            var data = await _mediator.Send(new GetAllIncomeTypes.Query());
            return Ok(data);
        }


        [HttpPost("incometypes")]
        public async Task<IActionResult> CreateIncomeType(CreateIncomeTypeDto createIncomeType)
        {
            var validator = _validationFactory.GetValidator<CreateIncomeTypeDto>();
            var result = validator.Validate(createIncomeType);
            if(!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return ValidationProblem
                (
                    title:"Validation problem",
                    statusCode: StatusCodes.Status400BadRequest 
                );
            }
            var newIncomeType = await _mediator.Send(new CreateIncomeType.Command { incomeType = createIncomeType });
            return CreatedAtAction(nameof(CreateIncomeType), newIncomeType);
        }


    }
    
}
