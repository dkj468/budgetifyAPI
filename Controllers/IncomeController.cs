using budgetifyAPI.Dtos;
using budgetifyAPI.Repository.Incomes;
using Microsoft.AspNetCore.Mvc;

namespace budgetifyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeRepository _incomeRepo;
        public IncomeController(IIncomeRepository incomeRepo)
        {
            _incomeRepo = incomeRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome(CreateIncomeDto income)
        {
            if(income.AccountId == -1)
            {
                ModelState.AddModelError("accountId", "Account id is required");
            }
            if (income.IncomeTypeId == -1)
            {
                ModelState.AddModelError("incomeTypeId", "Income type id is required");
            }
            if (income.Amount <= 0)
            {
                ModelState.AddModelError("amount", "Amount must be greater than zero");
            }

            if(ModelState.ErrorCount > 0)
            {
                return ValidationProblem
                (
                    title: "Validation problem",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }
            var newIncome = await _incomeRepo.CreateIncome(income);
            return CreatedAtAction(nameof(CreateIncome), newIncome);
        }


        [HttpGet("incometypes")]
        public async Task<IActionResult> GetAllIncomeTypes()
        {
            var data = await _incomeRepo.GetAllIncomeType();
            return Ok(data);
        }


        [HttpPost("incometypes")]
        public async Task<IActionResult> CreateIncomeType(CreateIncomeTypeDto createIncomeType)
        {
            // TODO --- Add validation for income type name
            var newIncomeType = await _incomeRepo.CreateIncomeType(createIncomeType);
            return CreatedAtAction(nameof(CreateIncomeType), newIncomeType);
        }


    }
    
}
