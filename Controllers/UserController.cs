using budgetifyAPI.Data;
using budgetifyAPI.Dtos;
using budgetifyAPI.Models;
using budgetifyAPI.Repository.Users;
using budgetifyAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace budgetifyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly ISignInHelper _signInHelper;
        private readonly TokenService _tokenService;
        public UserController(IUserRepository userRepo, ISignInHelper signInHelper, TokenService tokenService)
        {
            _userRepo = userRepo;
            _signInHelper = signInHelper;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login (LoginUserDto loginUserDto)
        {
            // get user from database
            var user = await _userRepo.GetUserByEmail(loginUserDto.Email);
            if (user == null)
            {
                return ValidationProblem(
                   title: "Authentication error",
                   detail: "cannot find user with given email id",
                   statusCode: StatusCodes.Status401Unauthorized
               );
            }
            // verify if password is correct
            if (!_signInHelper.VerifyPassword (user, user.Password, loginUserDto.Password))
            {
                return ValidationProblem(
                    title:"Authentication error",
                    detail:"Password is not correct",
                    statusCode: StatusCodes.Status401Unauthorized
                );
            }

            // create new token 
            // create token
            var userDto = new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
            // send user details
            return Ok(userDto);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto)
        {
            // verify if email is not already exists
            var user = await _userRepo.GetUserByEmail(registerUserDto.Email);
            if (user != null)
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

            // create new user object
            var newUser = new User
            {
                DisplayName = registerUserDto.Name,
                Email = registerUserDto.Email,
            };
            newUser.Password = _signInHelper.GeneratePassword(newUser, registerUserDto.Password);
            // create token
            var userDto = new UserDto
            {
                DisplayName = newUser.DisplayName,
                Email = newUser.Email,
                Token = _tokenService.CreateToken(newUser)
            };

            // save user in database
            await _userRepo.CreateNewUser(newUser);
          
            // send user details
            return Ok(userDto);
        }

        [HttpGet("currentuser")]
        public  ActionResult<UserDto> GetCurrentUser()
        {
            var userDto = new UserDto
            {
                DisplayName = _userRepo.User.DisplayName,
                Email = _userRepo.User.Email,
                Token = _tokenService.CreateToken(_userRepo.User)
            };
            return Ok(userDto);
        }
    }
}
