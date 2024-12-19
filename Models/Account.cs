namespace budgetifyAPI.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public decimal Balance { get; set; } = 0;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
