using budgetifyAPI.Dtos;
using budgetifyAPI.Models;

namespace budgetifyAPI.Repository.Incomes
{
    public interface IIncomeRepository
    {
        Task CreateIncome (Income Income, bool IsSave = true);
        Task<ICollection<IncomeType>> GetAllIncomeType ();
        Task<IncomeType> GetIncomeTypeById (int id);
        Task CreateIncomeType (IncomeType incomeType, bool IsSave = true);
    }
}
