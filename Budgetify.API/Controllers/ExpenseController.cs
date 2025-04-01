using budgetify.Application.Areas.Expenses;
using budgetify.Application.Dtos;
using budgetifyAPI.Factories;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace budgetifyAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {        
        private readonly IValidationFactory _validationFactory;
        private readonly IMediator _mediator;

        public ExpenseController(IValidationFactory validationFactory, IMediator mediator)
        {           
            _validationFactory = validationFactory;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExpenses()
        {
            var data = await _mediator.Send(new GetAllExpenses.Query());
            return Ok(data);
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
            var newExpense = await _mediator.Send(new CreateExpense.Request { expense = expense });
            return CreatedAtAction (nameof(CreateExpense) ,newExpense);
        }

        [HttpGet("expensetypes")]
        public async Task<IActionResult> GetAllExpenseTypes()
        {
            return Ok(await _mediator.Send (new GetAllExpenseType.Query()));
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
            var newExpenseType = await _mediator.Send (new CreateExpenseType.Command { expenseType = createExpenseType });
            return CreatedAtAction(nameof(createExpenseType), newExpenseType);
        }

        [HttpGet("expensecategory")]
        public async Task<IActionResult> GetAllExpenseCategories()
        {
            return Ok(await _mediator.Send (new GetAllExpenseCategories.Query ()));
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
            var newExpenseCategory = await _mediator.Send (new CreateExpenseCategory.Command { expenseCategory = createExpenseCategory });
            return CreatedAtAction(nameof(CreateExpenseCategory), newExpenseCategory);
        }
    }
}
