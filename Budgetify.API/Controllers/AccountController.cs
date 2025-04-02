using budgetify.Application.Areas.Accounts;
using budgetify.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace budgetifyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounst()
        {
            var data = await _mediator.Send(new GetAllAccounts.Query());
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
            var createdAccount = await _mediator.Send (new CreateAccount.Command { account = createAccountDto});
            return CreatedAtAction(nameof(CreateAccount), createdAccount);
        }
    }
    
}
