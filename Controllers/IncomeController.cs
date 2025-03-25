using budgetifyAPI.Dtos;
using budgetifyAPI.Factories;
using budgetifyAPI.Models;
using budgetifyAPI.Repository.Incomes;
using budgetifyAPI.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace budgetifyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        private readonly IValidationFactory _validationFactory;
        public IncomeController(IIncomeService incomeService,IValidationFactory validationFactory)
        {
            _incomeService = incomeService;
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
           
            var newIncome = await _incomeService.CreateIncome(income);
            return CreatedAtAction(nameof(CreateIncome), newIncome);
        }


        [HttpGet("incometypes")]
        public async Task<IActionResult> GetAllIncomeTypes()
        {
            var data = await _incomeService.GetAllIncomeType();
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
            var newIncomeType = await _incomeService.CreateIncomeType (createIncomeType);
            return CreatedAtAction(nameof(CreateIncomeType), newIncomeType);
        }


    }
    
}
