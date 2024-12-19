using budgetifyAPI.Models;

namespace budgetifyAPI.Dtos
{
    public class ExpenseTypesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ExpenseCategoryDto> ExpenseCategories { get; set; }
    }
}
