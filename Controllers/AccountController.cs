using budgetifyAPI.Repository.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace budgetifyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepo;
        public AccountController(IAccountRepository accountRepo)
        {
            _accountRepo = accountRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounst()
        {
            var data = await _accountRepo.GetAllAccounts();
            return Ok(data);
        }
    }
    
}
