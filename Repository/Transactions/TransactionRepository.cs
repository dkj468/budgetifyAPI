using budgetifyAPI.Data;
using budgetifyAPI.Dtos;
using budgetifyAPI.Repository.Users;
using Microsoft.EntityFrameworkCore;

namespace budgetifyAPI.Repository.Transactions
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _ctx;
        private readonly IUserRepository _userRepo;
        public TransactionRepository(DataContext ctx, IUserRepository userRepo)
        {
            _ctx = ctx;
            _userRepo = userRepo;
        }

        public async Task<ICollection<TransactionDto>> GetAllTransactions()
        {
            var transactions = await _ctx.AccountTransactions
                                    .Include(at => at.Account)
                                    .Where(at => at.UserId == _userRepo.User.Id)
                                    .ToListAsync();
            var ThisTransactionsList = new List<TransactionDto>();
            foreach (var transaction in transactions)
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
