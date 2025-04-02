
using Budgetify.Domain.Entities;

namespace budgetify.Application.Dtos
{
    public class IncomeDto
    {

        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int IncomeTypeId { get; set; }
        public string IncomeType { get; set; }
        public string? Account { get; set; }
        public int? AccountId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public TransactionDto Transaction { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; } 
    }
}
