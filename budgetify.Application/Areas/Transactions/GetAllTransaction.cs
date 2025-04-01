using budgetify.Application.Repositories;
using Budgetify.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budgetify.Application.Areas.Transactions
{
    public class GetAllTransaction
    {
        public class Query : IRequest<List<AccountTransaction>> { }

        public class Handler : IRequestHandler<Query, List<AccountTransaction>>
        {
            private readonly ITransactionRepository _transactionRepo;
            public Handler(ITransactionRepository transactionRepository)
            {
                _transactionRepo = transactionRepository;
            }
            public async Task<List<AccountTransaction>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _transactionRepo.GetAllTransactions();
            }
        }
    }
}
