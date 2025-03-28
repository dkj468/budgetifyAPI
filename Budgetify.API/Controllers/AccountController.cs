using budgetifyAPI.Dtos;
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

        [HttpPost]
        public async Task<IActionResult> CreateAccount (CreateAccountDto createAccountDto)
        {
            if(createAccountDto.Name.Trim() == string.Empty)
            {
                ModelState.AddModelError("name", "Account name cannot be empty");
            }

            if (ModelState.Count > 0)
            {
                return ValidationProblem
                (
                    title: "Validation problem",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }

            var createdAccount = await _accountRepo.CreateAccount(createAccountDto);
            return CreatedAtAction(nameof(CreateAccount), createdAccount);
        }
    }
    
}
