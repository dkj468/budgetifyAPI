namespace budgetifyAPI.Models
{
    public class ExpenseType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ExpenseCategory> ExpenseCategories    { get; set; }
    }
}
