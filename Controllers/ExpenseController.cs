using budgetifyAPI.Dtos;
using budgetifyAPI.Models;
using budgetifyAPI.Repository.Expenses;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace budgetifyAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepo;
        private readonly IValidator<CreateExpenseDto> _validatorExpense;
        private readonly IValidator<CreateExpenseTypeDto> _validatorExpenseType;
        private readonly IValidator<CreateExpenseCategoryDto> _validatorExpenseCategory;

        public ExpenseController(IExpenseRepository expenseRepo, 
                IValidator<CreateExpenseDto> validatorExpense, 
                IValidator<CreateExpenseTypeDto> validatorExpenseType, 
                IValidator<CreateExpenseCategoryDto> validatorExpenseCategory)
        {
            _expenseRepo = expenseRepo;
            _validatorExpense = validatorExpense;
            _validatorExpenseType = validatorExpenseType;
            _validatorExpenseCategory = validatorExpenseCategory;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            return Ok(await _expenseRepo.GetAllExpenses());
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(CreateExpenseDto expense)
        {
            var validationRsult = _validatorExpense.Validate(expense);
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
            var validationRsult = _validatorExpenseType.Validate(createExpenseType);
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
            var validationRsult = _validatorExpenseCategory.Validate(createExpenseCategory);
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
