using Budgetify.Domain.Enums;

namespace Budgetify.Domain.Entities
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int ExpenseTypeId { get; set; }
        public ExpenseType ExpenseType { get; set; }
        public AddedBy AddedBy { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
