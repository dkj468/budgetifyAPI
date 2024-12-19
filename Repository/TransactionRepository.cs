using budgetifyAPI.Data;
using budgetifyAPI.Dtos;
using budgetifyAPI.Enums;
using budgetifyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace budgetifyAPI.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DataContext _ctx;
        public TransactionRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async  Task<ICollection<TransactionDto>> GetAllTransactions()
        {
            var transactions = await _ctx.AccountTransactions
                                    .Include(at => at.Account)
                                    .ToListAsync();
            var ThisTransactionsList = new List<TransactionDto>();
            foreach (var transaction in transactions)
            {
                var transactionDto = new TransactionDto
                {
                    Id = transaction.Id,
                    AccountId = transaction.AccountId,
                    AccountName = transaction.Account.Name,
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
