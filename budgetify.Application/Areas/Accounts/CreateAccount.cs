using budgetify.Application.Dtos;
using budgetify.Application.Repositories;
using Budgetify.Domain.Entities;
using Budgetify.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budgetify.Application.Areas.Accounts
{
    public class CreateAccount
    {
        public class Command : IRequest<Account>
        {
            public CreateAccountDto account;
        }

        public class Handler : IRequestHandler<Command, Account>
        {
            private readonly IAccountRepository _accountRepo;
            private readonly IUserRepository _userRepo;
            public Handler(IAccountRepository accountRepository, IUserRepository userRepo)
            {
                _accountRepo = accountRepository;
                _userRepo = userRepo;   
            }
            public async Task<Account> Handle(Command request, CancellationToken cancellationToken)
            {
                var newAccount = new Account
                {
                    Name = request.account.Name,
                    Description = request.account.Description,
                    Balance = request.account.Balance,
                    ImageUrl = request.account.ImageUrl,
                    UserId = _userRepo.User.Id,
                    AddedBy = AddedBy.System
                };
                await _accountRepo.CreateAccount(newAccount);                
                return newAccount;
            }
        }
    }
}
