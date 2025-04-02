namespace Budgetify.Domain.Entities
{
    public class Income
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int IncomeTypeId { get; set; }
        public IncomeType IncomeType { get; set; }               
        public Account? Account { get; set; }
        public int? AccountId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public AccountTransaction Transaction { get; set; }
        public int TransactionId { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}
