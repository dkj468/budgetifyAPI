using budgetifyAPI.Dtos;
using budgetifyAPI.Models;

namespace budgetifyAPI.Services
{
    public interface IIncomeService
    {
        Task<IncomeDto> CreateIncome (CreateIncomeDto Income);
        Task<ICollection<IncomeType>> GetAllIncomeType();
        Task<IncomeType> CreateIncomeType (CreateIncomeTypeDto incomeType);
    }
}
