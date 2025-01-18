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
            await _incomeRepo.CreateIncome(income);
            return Ok();
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
