using budgetifyAPI.Dtos;
using budgetifyAPI.Repository.Expenses;
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

        public ExpenseController(IExpenseRepository expenseRepo)
        {
            _expenseRepo = expenseRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            return Ok(await _expenseRepo.GetAllExpenses());
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense(CreateExpenseDto expense)
        {
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
            // TODO --- Add validation for expense type Name
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
            // TODO --- Add validation for expense category Name,Expense type id
            var newExpenseCategory = await _expenseRepo.CreateExpenseCategory(createExpenseCategory);
            return CreatedAtAction(nameof(CreateExpenseCategory), newExpenseCategory);
        }
    }
}
