using budgetifyAPI.Dtos;
using budgetifyAPI.Models;

namespace budgetifyAPI.Repository.Incomes
{
    public interface IIncomeRepository
    {
        Task CreateIncome(CreateIncomeDto Income);
        Task<ICollection<IncomeType>> GetAllIncomeType();
        Task<IncomeType> CreateIncomeType(CreateIncomeTypeDto createIncomeType);
    }
}
