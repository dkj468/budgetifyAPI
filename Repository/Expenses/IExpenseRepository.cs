﻿using budgetifyAPI.Dtos;
using budgetifyAPI.Models;

namespace budgetifyAPI.Repository.Expenses
{
    public interface IExpenseRepository
    {
        Task<ICollection<ExpenseDto>> GetAllExpenses();
        Task<ICollection<ExpenseTypesDto>> GetAllExpenseTypes();
        Task<ExpenseTypesDto> CreateExpenseType(CreateExpenseTypeDto createExpenseType);
        Task<ICollection<ExpenseCategoryDto>> GetAllExpenseCategories();
        Task<ExpenseCategoryDto> CreateExpenseCategory(CreateExpenseCategoryDto createExpenseCategory);
        Task<ExpenseCategory> GetExpenseCategoryById(int id);
        Task<ExpenseType> GetExpenseTypeById(int id);
        Task CreateExpense(Expense expense, bool IsSave = true);
    }
}
