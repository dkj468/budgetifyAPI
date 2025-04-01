namespace budgetify.Application.Dtos
{
    public class CreateAccountDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Balance { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
