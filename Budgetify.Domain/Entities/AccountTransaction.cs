using Budgetify.Domain.Enums;

namespace Budgetify.Domain.Entities
{
    public class AccountTransaction
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public Account? Account { get; set; }
        public int? AccountId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public decimal ClosingBalance { get; set; } = 0;

    }
}
