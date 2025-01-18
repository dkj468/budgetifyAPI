using budgetifyAPI.Dtos;

namespace budgetifyAPI.Repository.Expenses
{
    public interface IExpenseRepository
    {
        Task<ICollection<ExpenseDto>> GetAllExpenses();
        Task<ICollection<ExpenseTypesDto>> GetAllExpenseTypes();
        Task<ExpenseTypesDto> CreateExpenseType(CreateExpenseTypeDto createExpenseType);
        Task<ICollection<ExpenseCategoryDto>> GetAllExpenseCategories();
        Task<ExpenseCategoryDto> CreateExpenseCategory(CreateExpenseCategoryDto createExpenseCategory);
        Task CreateExpense(CreateExpenseDto expense);
    }
}
