using budgetifyAPI.Dtos;
using budgetifyAPI.Models;

namespace budgetifyAPI.Repository
{
    public interface IIncomeRepository
    {
        Task CreateIncome(CreateIncomeDto Income);
        Task<ICollection<IncomeType>> GetAllIncomeType();
    }
}
