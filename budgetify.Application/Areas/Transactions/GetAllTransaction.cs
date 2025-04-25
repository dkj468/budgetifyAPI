using budgetify.Application.Dtos;
using budgetify.Application.Repositories;
using MediatR;


namespace budgetify.Application.Areas.Transactions
{
    public class GetAllTransaction
    {
        public class Query : IRequest<List<TransactionDto>> { }

        public class Handler : IRequestHandler<Query, List<TransactionDto>>
        {
            private readonly ITransactionRepository _transactionRepo;
            public Handler(ITransactionRepository transactionRepository)
            {
                _transactionRepo = transactionRepository;
            }
            public async Task<List<TransactionDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var data = await _transactionRepo.GetAllTransactions();
                var ThisTransactionsList = new List<TransactionDto>();
                foreach (var transaction in data)
                {
                    var transactionDto = new TransactionDto
                    {
                        Id = transaction.Id,
                        AccountId = transaction.AccountId,
                        AccountName = transaction.Account != null ? transaction.Account.Name : "",
                        Type = transaction.Type.ToString(),
                        DateCreated = transaction.DateCreated,
                        DateUpdated = transaction.DateUpdated,
                        Description = transaction.Description,
                        Amount = transaction.Amount,
                        ClosingBalance = transaction.ClosingBalance,
                    };
                    ThisTransactionsList.Add(transactionDto);
                }
                return ThisTransactionsList;
            }
        }
    }
}
