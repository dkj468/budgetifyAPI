using budgetify.Application.Repositories;
using budgetify.Persistence.Contexts;
using Budgetify.Domain.Entities;
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

        public async Task<List<AccountTransaction>> GetAllTransactions()
        {
            var transactions = await _ctx.AccountTransactions
                                    .Include(at => at.Account)
                                    .Where(at => at.UserId == _userRepo.User.Id)
                                    .ToListAsync();
            return transactions;
        }

        public async Task CreateNewTransaction (AccountTransaction accountTransaction, bool IsSave = true)
        {
            _ctx.AccountTransactions.Add (accountTransaction);
            if (IsSave)
            {
                await _ctx.SaveChangesAsync ();
            }
        }
    }
}
