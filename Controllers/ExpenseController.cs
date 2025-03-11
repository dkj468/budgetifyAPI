using budgetifyAPI.Dtos;
using budgetifyAPI.Factories;
using budgetifyAPI.Repository.Expenses;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace budgetifyAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepo;
        private readonly IValidationFactory _validationFactory;

        public ExpenseController(IExpenseRepository expenseRepo, IValidationFactory validationFactory)
        {
            _expenseRepo = expenseRepo;
            _validationFactory = validationFactory;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            return Ok(await _expenseRepo.GetAllExpenses());
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
            var newExpense =  await _expenseRepo.CreateExpense(expense);
            return CreatedAtAction (nameof(CreateExpense) ,newExpense);
        }

        [HttpGet("expensetypes")]
        public async Task<IActionResult> GetAllExpenseTypes()
        {
            return Ok(await _expenseRepo.GetAllExpenseTypes());
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
            var newExpenseType = await _expenseRepo.CreateExpenseType(createExpenseType);
            return CreatedAtAction(nameof(createExpenseType), newExpenseType);
        }

        [HttpGet("expensecategory")]
        public async Task<IActionResult> GetAllExpenseCategories()
        {
            return Ok(await _expenseRepo.GetAllExpenseCategories());
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
            var newExpenseCategory = await _expenseRepo.CreateExpenseCategory(createExpenseCategory);
            return CreatedAtAction(nameof(CreateExpenseCategory), newExpenseCategory);
        }
    }
}
