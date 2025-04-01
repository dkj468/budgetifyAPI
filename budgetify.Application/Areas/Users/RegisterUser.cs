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
            public Handler(IUserRepository userRepository, IAuthService authService, ITokenService tokenService)
            {
                _userRepo = userRepository;
                _authService = authService;
                _tokenService = tokenService;
            }
            public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var dbUser = await _userRepo.GetUserByEmail(request.user.Email);
                if ( dbUser == null)
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
                    DisplayName = dbUser.DisplayName,
                    Email = dbUser.Email,
                    Token = _tokenService.CreateToken(dbUser)
                };

                return user;
            }
        }
    }
}
