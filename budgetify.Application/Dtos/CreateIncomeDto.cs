namespace budgetify.Application.Dtos
{
    public class CreateIncomeDto
    {
        public decimal Amount { get; set; }
        public int IncomeTypeId { get; set; }        
        public int AccountId { get; set; }
        public string Description { get; set; }
    }
}
