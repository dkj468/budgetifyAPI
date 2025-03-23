﻿using budgetifyAPI.Data;
using budgetifyAPI.Dtos;
using budgetifyAPI.Enums;
using budgetifyAPI.Models;
using budgetifyAPI.Repository.Accounts;
using budgetifyAPI.Repository.Users;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace budgetifyAPI.Repository.Expenses
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly DataContext _ctx;       
        private readonly IUserRepository _userRepo;
        public ExpenseRepository(DataContext ctx, IUserRepository userRepo)
        {
            _ctx = ctx;          
            _userRepo = userRepo;
        }

        public async Task CreateExpense (Expense expense, bool IsSave = true)
        {
            _ctx.Expenses.Add(expense);
            if (IsSave)
            {
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Expense>> GetAllExpenses()
        {
            var expenses = await _ctx.Expenses
                .Include(e => e.ExpenseType)
                .Include(e => e.ExpenseCategory)
                .Include(e => e.Account)
                .Include(e => e.Transaction)
                .Where(e => e.UserId == _userRepo.User.Id)
                .ToListAsync();
            return expenses;
        }

        public async Task<ICollection<ExpenseType>> GetAllExpenseTypes()
        {
            var data = await _ctx.ExpenseTypes
                            .Include(et => et.ExpenseCategories)
                            .Where(et => et.UserId == _userRepo.User.Id)
                            .ToListAsync();
            return data;            
        }

        public async Task<ICollection<ExpenseCategory>> GetAllExpenseCategories()
        {
            var data = await _ctx.ExpenseCategories
                            .Include(ec => ec.ExpenseType)
                            .Where(ec => ec.UserId == _userRepo.User.Id)
                            .ToListAsync();
            return data;
        }

        public async Task CreateExpenseType (ExpenseType expenseType, bool IsSave = true)
        {
            _ctx.ExpenseTypes.Add(expenseType);
            if (IsSave)
            {
                await _ctx.SaveChangesAsync();
            }            
        }

        public async Task CreateExpenseCategory (ExpenseCategory expenseCategory, bool IsSave = true)
        {
            _ctx.ExpenseCategories.Add(expenseCategory);
            if (IsSave)
            {
                await _ctx.SaveChangesAsync();
            }            
        }

        public async Task<ExpenseCategory> GetExpenseCategoryById (int id)
        {
            return await _ctx.ExpenseCategories.FindAsync(id);
        }

        public async Task<ExpenseType> GetExpenseTypeById (int id)
        {
            return await _ctx.ExpenseTypes.FindAsync(id);
        }
    }
}
