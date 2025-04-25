using budgetify.Application.Dtos;
using budgetify.Application.Repositories;
using budgetify.Application.Services;
using Budgetify.Domain.Entities;
using MediatR;


namespace budgetify.Application.Areas.Users
{
    public class RegisterUser
    {
        public class Command : IRequest<UserDto>
        {
            public RegisterUserDto user;
        }

        public class Handler : IRequestHandler<Command, UserDto>
        {
            private readonly IUserRepository _userRepo;
            private readonly IAuthService _authService;
            private readonly ITokenService _tokenService;
            private readonly IEmailBackgroundService _emailBackgroundService;
            public Handler(IUserRepository userRepository, IAuthService authService, ITokenService tokenService, IEmailBackgroundService emailBackgroundService)
            {
                _userRepo = userRepository;
                _authService = authService;
                _tokenService = tokenService;
                _emailBackgroundService =   emailBackgroundService;
            }
            public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var dbUser = await _userRepo.GetUserByEmail(request.user.Email);
                if ( dbUser != null)
                {
                    return null;
                }

                // create new user object
                var newUser = new User
                {
                    DisplayName = request.user.Name,
                    Email = request.user.Email,
                };
                newUser.Password = _authService.GeneratePassword(request.user.Password);
                await _userRepo.CreateNewUser(newUser);
                var user = new UserDto
                {
                    DisplayName = newUser.DisplayName,
                    Email = newUser.Email,
                    Token = _tokenService.CreateToken(newUser)
                };

                // send email in background
                var emailJob = new EmailJob
                {
                    To = request.user.Email,
                    Subject = "Verification code (Valid for 10 minutes)",
                    VerificationCode = new Random().Next(100000, 999999).ToString()
                };
                _emailBackgroundService.SendEmail(emailJob);

                return user;
            }
        }
    }
}
