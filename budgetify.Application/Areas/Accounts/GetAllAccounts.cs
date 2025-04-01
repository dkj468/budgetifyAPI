using budgetify.Application.Repositories;
using Budgetify.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budgetify.Application.Areas.Accounts
{
    public class GetAllAccounts
    {
        public class Query: IRequest<List<Account>> { }

        public class Handler : IRequestHandler<Query, List<Account>>
        {
            private readonly IAccountRepository _accountRepo;
            public Handler(IAccountRepository accountRepository)
            {
                _accountRepo = accountRepository;
            }
            public async Task<List<Account>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _accountRepo.GetAllAccounts();
            }
        }
    }
}
