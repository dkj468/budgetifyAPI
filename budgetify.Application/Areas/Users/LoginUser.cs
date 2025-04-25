using budgetify.Application.Dtos;
using budgetify.Application.Repositories;
using budgetify.Application.Services;
using MediatR;

namespace budgetify.Application.Areas.Users
{
    public class LoginUser
    {
        public class Command : IRequest<UserDto>
        {
            public LoginUserDto user;
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
                // get user from database
                var dbUser = await _userRepo.GetUserByEmail (request.user.Email);
                if (dbUser == null) return null;

                // verify password 
                if (!_authService.VerifyPassword (dbUser.Password, request.user.Password))
                {
                    return null;
                }

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
