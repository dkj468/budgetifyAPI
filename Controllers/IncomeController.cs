using budgetifyAPI.Dtos;
using budgetifyAPI.Repository;
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


    }
    
}
