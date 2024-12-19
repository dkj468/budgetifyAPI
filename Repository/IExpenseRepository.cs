using budgetifyAPI.Dtos;

namespace budgetifyAPI.Repository
{
    public interface IExpenseRepository
    {
        Task<ICollection<ExpenseDto>> GetAllExpenses();
        Task<ICollection<ExpenseTypesDto>> GetAllExpenseTypes();
        Task<ICollection<ExpenseCategoryDto>> GetAllExpenseCategories();
        Task CreateExpense(CreateExpenseDto expense);
    }
}
