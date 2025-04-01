namespace budgetify.Application.Dtos
{
    public class CreateExpenseDto
    {
        public decimal Amount { get; set; }
        public int ExpenseTypeId { get; set; }
        public int ExpenseCategoryId { get; set; }        
        public int AccountId { get; set; }
        public string Description { get; set; }
    }
}
