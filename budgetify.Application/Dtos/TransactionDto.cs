

namespace budgetify.Application.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public string AccountName { get; set; }
        public int? AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public decimal ClosingBalance { get; set; }
    }
}
