using budgetify.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budgetify.Application.Areas.Users
{
    public class ValidateEmailVerificationCode
    {

        public class Command: IRequest<bool>
        {
            public string code;
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly IUserRepository _userRepo;
            public Handler(IUserRepository userRepository)
            {
                _userRepo = userRepository;
            }
            public Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                // get logged in user
                var user = _userRepo.User;
                if (user.EmailVerificationCode == request.code && user.EmailVerificationDateTime > DateTime.Now)
                {
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
        }
    }
}
