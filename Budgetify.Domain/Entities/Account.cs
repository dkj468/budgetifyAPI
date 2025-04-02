using Budgetify.Domain.Enums;

namespace Budgetify.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; } 
        public decimal Balance { get; set; } = 0;
        public string? ImageUrl { get; set; }
        public AddedBy AddedBy { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
