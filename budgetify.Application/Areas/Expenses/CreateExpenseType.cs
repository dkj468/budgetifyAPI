using budgetify.Application.Dtos;
using budgetify.Application.Helpers;
using budgetify.Application.Repositories;
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

namespace budgetify.Application.Areas.Expenses
{
    public class CreateExpenseType
    {
        public class Command : IRequest<ExpenseTypesDto>
        {
            public CreateExpenseTypeDto expenseType;
        }

        public class Handler : IRequestHandler<Command, ExpenseTypesDto>
        {
            private readonly IExpenseRepository _expenseRepo;
            private readonly IUserRepository _userRepo;

            public Handler(IExpenseRepository expenseRepository, IUserRepository userRepository)
            {
                _expenseRepo = expenseRepository;
                _userRepo = userRepository;
            }
            public async Task<ExpenseTypesDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var newExpenseType = new ExpenseType
                {
                    Name = request.expenseType.Name,
                    Description = request.expenseType.Description,
                    AddedBy = AddedBy.User,
                    UserId = _userRepo.User.Id,
                };

                await _expenseRepo.CreateExpenseType(newExpenseType);

                return new ExpenseTypesDto
                {
                    Id = newExpenseType.Id,
                    Name = newExpenseType.Name,
                    Description = newExpenseType.Description
                };
            }
        }
    }
}
