using budgetify.Application.Dtos;
using Budgetify.Domain.Entities;

namespace budgetify.Application.Repositories
{
    public interface IIncomeRepository
    {
        Task CreateIncome (Income Income, bool IsSave = true);
        Task<List<IncomeType>> GetAllIncomeType ();
        Task<IncomeType> GetIncomeTypeById (int id);
        Task CreateIncomeType (IncomeType incomeType, bool IsSave = true);
        Task<IncomeValidationDto> ValidateIncome(CreateIncomeDto income);

    }
}
