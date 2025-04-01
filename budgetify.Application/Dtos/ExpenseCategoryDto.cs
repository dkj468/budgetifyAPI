namespace budgetify.Application.Dtos
{
    public class ExpenseCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; } = "";
        public string ExpenseTypeDescription { get; set; } = "";
    }
}
