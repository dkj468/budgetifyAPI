using budgetify.Application.Dtos;
using budgetify.Application.Repositories;
using Budgetify.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budgetify.Application.Areas.Incomes
{
    public class GetAllIncomeTypes
    {
        public class Query : IRequest<List<IncomeType>> { }

        public class Handler : IRequestHandler<Query, List<IncomeType>>
        {
            private readonly IIncomeRepository _incomeRepo;
            public Handler(IIncomeRepository incomeRepository)
            {
                _incomeRepo = incomeRepository;
            }
            public async Task<List<IncomeType>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _incomeRepo.GetAllIncomeType();
            }
        }
    }
}
