using budgetifyAPI.Dtos;
using budgetifyAPI.Factories;
using budgetifyAPI.Repository.Expenses;
using budgetifyAPI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace budgetifyAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {        
        private readonly IValidationFactory _validationFactory;
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService, IValidationFactory validationFactory)
        {           
            _validationFactory = validationFactory;
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            return Ok(await _expenseService.GetAllExpenses());
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(CreateExpenseDto expense)
        {
            var validator = _validationFactory.GetValidator<CreateExpenseDto>();
            var validationRsult = validator.Validate(expense);
            if(!validationRsult.IsValid)
            {
                validationRsult.AddToModelState(this.ModelState);
                return ValidationProblem
                (
                    title:"Validation problem",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }
            var newExpense =  await _expenseService.CreateExpense (expense);
            return CreatedAtAction (nameof(CreateExpense) ,newExpense);
        }

        [HttpGet("expensetypes")]
        public async Task<IActionResult> GetAllExpenseTypes()
        {
            return Ok(await _expenseService.GetAllExpenseTypes());
        }

        [HttpPost("expensetypes")]
        public async Task<IActionResult> CreateExpenseType(CreateExpenseTypeDto createExpenseType)
        {
            var validator = _validationFactory.GetValidator<CreateExpenseTypeDto>();
            var validationRsult = validator.Validate(createExpenseType);
            if (!validationRsult.IsValid)
            {
                validationRsult.AddToModelState(this.ModelState);
                return ValidationProblem
                (
                    title: "Validation problem",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }
            var newExpenseType = await _expenseService.CreateExpenseType(createExpenseType);
            return CreatedAtAction(nameof(createExpenseType), newExpenseType);
        }

        [HttpGet("expensecategory")]
        public async Task<IActionResult> GetAllExpenseCategories()
        {
            return Ok(await _expenseService.GetAllExpenseCategories());
        }

        [HttpPost("expensecategory")]
        public async Task<IActionResult> CreateExpenseCategory(CreateExpenseCategoryDto createExpenseCategory)
        {
            var validator = _validationFactory.GetValidator<CreateExpenseCategoryDto>();
            var validationRsult = validator.Validate(createExpenseCategory);
            if (!validationRsult.IsValid)
            {
                validationRsult.AddToModelState(this.ModelState);
                return ValidationProblem
                (
                    title: "Validation problem",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }
            var newExpenseCategory = await _expenseService.CreateExpenseCategory (createExpenseCategory);
            return CreatedAtAction(nameof(CreateExpenseCategory), newExpenseCategory);
        }
    }
}
