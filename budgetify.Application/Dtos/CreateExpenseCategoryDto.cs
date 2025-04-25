namespace budgetify.Application.Dtos
{
    public class CreateExpenseCategoryDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int ExpenseTypeId { get; set; }
    }
}
