﻿using budgetifyAPI.Data;
using budgetifyAPI.Dtos;
using budgetifyAPI.Enums;
using budgetifyAPI.Exceptions;
using budgetifyAPI.Models;
using budgetifyAPI.Repository.Accounts;
using budgetifyAPI.Repository.Users;
using Microsoft.EntityFrameworkCore;

namespace budgetifyAPI.Repository.Incomes
{
    public class IncomeRepostory : IIncomeRepository
    {
        private readonly DataContext _ctx;
        private readonly IAccountRepository _accountRepo;
        private readonly IUserRepository _userRepo;
        public IncomeRepostory(DataContext ctx, IAccountRepository accountRepo, IUserRepository userRepo)
        {
            _ctx = ctx;
            _accountRepo = accountRepo;
            _userRepo = userRepo;
        }

        private AccountTransaction CreateTransaction(Account account, Income income)
        {

            var ThisTransaction = new AccountTransaction
            {
                Type = TransactionType.Income,
                AccountId = income.AccountId,
                Amount = income.Amount,
                Description = income.Description,
                ClosingBalance = account.Balance + income.Amount,
                UserId = _userRepo.User.Id
            };

            return ThisTransaction;
        }
        public async Task CreateIncome(Income income, bool IsSave = true)
        {
            _ctx.Incomes.Add(income);
            if (IsSave)
            {
                await _ctx.SaveChangesAsync();
            }                       
        }

        public async Task<ICollection<IncomeType>> GetAllIncomeType()
        {
            return await _ctx.IncomeTypes
                        .Where(it => it.UserId ==  _userRepo.User.Id)
                        .ToListAsync();
        }

        public async Task CreateIncomeType(IncomeType incomeType, bool IsSave = true)
        {
            _ctx.IncomeTypes.Add (incomeType);
            if (IsSave)
            {
                await _ctx.SaveChangesAsync();
            }
        }

        public async Task<IncomeType> GetIncomeTypeById(int id)
        {
           return await _ctx.IncomeTypes.FindAsync (id);
        }
    }
}
