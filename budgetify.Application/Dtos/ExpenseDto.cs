
namespace budgetify.Application.Dtos
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int ExpenseTypeId { get; set; }
        public string ExpenseType { get; set; }
        public int ExpenseCategoryId { get; set; }
        public string ExpenseCategory { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
        public int? AccountId { get; set; }
        public string AccountName { get; set; }
        public TransactionDto? Transaction { get; set; }
    }
}
