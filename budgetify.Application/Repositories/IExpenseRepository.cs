using budgetify.Application.Dtos;
using Budgetify.Domain.Entities;

namespace budgetify.Application.Repositories
{
    public interface IExpenseRepository
    {
        Task<ICollection<Expense>> GetAllExpenses();
        Task<ICollection<ExpenseType>> GetAllExpenseTypes();
        Task CreateExpenseType(ExpenseType expenseType, bool IsSave = true);
        Task<ICollection<ExpenseCategory>> GetAllExpenseCategories();
        Task CreateExpenseCategory(ExpenseCategory expenseCategory, bool IsSave = true);
        Task<ExpenseCategory> GetExpenseCategoryById(int id);
        Task<ExpenseType> GetExpenseTypeById(int id);
        Task CreateExpense(Expense expense, bool IsSave = true);
        Task<ExpenseValidationDto> ValidateExpense(CreateExpenseDto expense);
    }
}
