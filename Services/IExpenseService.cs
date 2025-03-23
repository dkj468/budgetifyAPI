using budgetifyAPI.Dtos;

namespace budgetifyAPI.Services
{
    public interface IExpenseService
    {
        Task<ICollection<ExpenseDto>> GetAllExpenses();
        Task<ICollection<ExpenseTypesDto>> GetAllExpenseTypes();
        Task<ExpenseTypesDto> CreateExpenseType(CreateExpenseTypeDto createExpenseType);
        Task<ICollection<ExpenseCategoryDto>> GetAllExpenseCategories();
        Task<ExpenseCategoryDto> CreateExpenseCategory(CreateExpenseCategoryDto createExpenseCategory);
        Task<ExpenseDto?> CreateExpense(CreateExpenseDto expense);
    }
}
