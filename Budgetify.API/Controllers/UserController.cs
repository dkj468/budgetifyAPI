using budgetify.Application.Areas.Users;
using budgetify.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace budgetifyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login (LoginUserDto loginUserDto)
        {
            // get user from database
            var user = await _mediator.Send(new LoginUser.Command { user = loginUserDto });
            if (user == null)
            {
                return ValidationProblem(
                   title: "Authentication error",
                   detail: "cannot find user with given email and password",
                   statusCode: StatusCodes.Status401Unauthorized
               );
            }            

            // send user details
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto)
        {
            // verify if email is not already exists
            var user = await _mediator.Send(new RegisterUser.Command { user = registerUserDto });
            if (user == null)
            {
                ModelState.AddModelError("email", "This email is already taken");
                //return ValidationProblem();
            }

            if (registerUserDto.Password.Length < 10)
            {
                ModelState.AddModelError("password", "password must contain atleast 10 characters");
                //return ValidationProblem();
            }

            if (ModelState.Count > 0) 
            {
                return ValidationProblem
                (
                    title:"Validation problem",
                    statusCode : StatusCodes.Status400BadRequest
                );
            }
            // send user details
            return Ok(user);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> ValidateEmailVerificationCode (string code)
        {
            if (code == null || code == "")
            {
                ModelState.AddModelError("code", "Verification code cannot be empty");                
            }
            if (ModelState.ErrorCount > 0)
            {
                return ValidationProblem
                (
                    title: "Validation problem",
                    statusCode: StatusCodes.Status400BadRequest
                );
            }

            var result = await _mediator.Send(new ValidateEmailVerificationCode.Command() { code = code });
            Object obj = new
            {
                IsVerified = result
            };
            return Ok(obj);
        }

        [HttpGet("currentuser")]
        public  ActionResult<UserDto> GetCurrentUser()
        {
            var user = _mediator.Send(new CurrentUser.Query());
            return Ok(user);
        }
    }
}
