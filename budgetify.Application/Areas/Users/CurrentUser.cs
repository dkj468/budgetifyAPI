using budgetify.Application.Dtos;
using budgetify.Application.Repositories;
using budgetify.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budgetify.Application.Areas.Users
{
    public class CurrentUser
    {
        public class Query : IRequest<UserDto> { }

        public class Handler : IRequestHandler<Query, UserDto>
        {
            private readonly ITokenService _tokenService;
            private readonly IUserRepository _userRepo;
            public Handler(ITokenService tokenService, IUserRepository userRepository)
            {
                _tokenService = tokenService;
                _userRepo = userRepository;
            }
            public Task<UserDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var user =  new UserDto
                {
                    DisplayName = _userRepo.User.DisplayName,
                    Email = _userRepo.User.Email,
                    Token = _tokenService.CreateToken(_userRepo.User)
                };

                return Task.FromResult(user);
            }
        }
    }
}
