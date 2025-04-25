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

namespace budgetify.Application.Areas.Incomes
{
    public class CreateIncomeType
    {
        public class Command : IRequest<IncomeType>
        {
            public CreateIncomeTypeDto incomeType;
        }

        public class Handler : IRequestHandler<Command, IncomeType>
        {
            private readonly IIncomeRepository _incomeRepo;
            private readonly IUserRepository _userRepo;
            public Handler (IIncomeRepository incomeRepository, IUserRepository userRepository)
            {
                _incomeRepo = incomeRepository;
                _userRepo = userRepository;
            }
            public async Task<IncomeType> Handle(Command request, CancellationToken cancellationToken)
            {

                var newIncomeType = new IncomeType
                {
                    Name = request.incomeType.Name,
                    Description = request.incomeType.Description,
                    AddedBy = AddedBy.User, // AddedBy enum
                    UserId = _userRepo.User.Id,
                };
                await _incomeRepo.CreateIncomeType(newIncomeType);

                return new IncomeType
                {
                    Id = newIncomeType.Id,
                    Name = newIncomeType.Name,
                    Description = newIncomeType.Description,
                };
            }
        }
    }
}
