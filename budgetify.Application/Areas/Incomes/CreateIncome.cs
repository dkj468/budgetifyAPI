using budgetify.Application.Dtos;
using budgetify.Application.Helpers;
using Budgetify.Domain.Commons.Exceptions;
using Budgetify.Domain.Entities;
using Budgetify.Domain.Enums;
using budgetifyAPI.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budgetify.Application.Areas.Incomes
{
    public class CreateIncome
    {
        public class Command : IRequest<IncomeDto>
        {
            public CreateIncomeDto income;
        }

        public class Handler : IRequestHandler<Command, IncomeDto>
        {
            private readonly IUnitOfWork _unitOfWork;
            public Handler (IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            public async Task<IncomeDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var incomeValidationResult = await _unitOfWork.incomeRepo.ValidateIncome(request.income);
                if (!incomeValidationResult.IsAccountExists)
                {
                    throw new NotFoundException("Account", request.income.AccountId);
                }
                if (!incomeValidationResult.IsIncomeTypeExists)
                {
                    throw new NotFoundException("Income Type", request.income.IncomeTypeId);
                }
                var ThisIncome = new Income
                {
                    IncomeTypeId = request.income.IncomeTypeId,
                    Amount = request.income.Amount,
                    AccountId = request.income.AccountId,
                    Description = request.income.Description,
                    UserId = _unitOfWork.userRepo.User.Id
                };
                var account = await _unitOfWork.accountRepo.GetAccountById (request.income.AccountId);
                var ThisTransaction = TransactionHelper.CreateTransaction(TransactionType.Income, account, request.income.Amount, request.income.Description, _unitOfWork.userRepo.User.Id);
                await _unitOfWork.transactionRepo.CreateNewTransaction(ThisTransaction, false);
                // update account balance --- account is being tracked in context
                account.Balance = account.Balance + request.income.Amount;
                ThisIncome.Transaction = ThisTransaction;
                await _unitOfWork.incomeRepo.CreateIncome(ThisIncome);
                await _unitOfWork.SaveChangesAsync();

                return new IncomeDto
                {
                    Id = ThisIncome.Id,
                    IncomeTypeId = ThisIncome.IncomeTypeId,
                    Amount = ThisIncome.Amount,
                    AccountId = account.Id,
                    Account = account.Description,
                    DateCreated = ThisIncome.DateCreated,
                    DateUpdated = ThisIncome.DateUpdated,
                    Description = ThisIncome.Description,
                    UserId = _unitOfWork.userRepo.User.Id,
                    Transaction = new TransactionDto
                    {
                        Id = ThisTransaction.Id,
                        AccountId = account.Id,
                        AccountName = account.Name,
                        Amount = ThisIncome.Amount,
                        ClosingBalance = ThisTransaction.ClosingBalance,
                        DateCreated = ThisTransaction.DateCreated,
                        DateUpdated = ThisTransaction.DateUpdated,
                        Description = ThisTransaction.Description,
                        Type = TransactionType.Income.ToString()
                    }

                };
            }
        }
    }
}
