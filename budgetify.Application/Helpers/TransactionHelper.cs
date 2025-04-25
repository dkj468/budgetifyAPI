using Budgetify.Domain.Entities;
using Budgetify.Domain.Enums;

namespace budgetify.Application.Helpers
{
    public static class TransactionHelper
    {
        public static AccountTransaction CreateTransaction (TransactionType type, Account account, decimal amount, string description, int userId)
        {
            return new AccountTransaction
            {
                Type = type,
                AccountId = account.Id,
                Amount = amount,
                Description = description,
                ClosingBalance = account.Balance - amount,
                UserId = userId
            };
        }
    }
}
