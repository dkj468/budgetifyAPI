﻿using budgetifyAPI.Repository.Accounts;
using budgetifyAPI.Repository.Expenses;
using budgetifyAPI.Repository.Transactions;
using budgetifyAPI.Repository.Users;

namespace budgetifyAPI.Services
{
    public interface IUnitOfWork
    {
        IExpenseRepository expenseRepo { get; }
        IAccountRepository accountRepo { get; }
        IUserRepository userRepo { get; }
        ITransactionRepository transactionRepo { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

    }
}
