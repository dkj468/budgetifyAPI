using System.Diagnostics.Contracts;

namespace budgetifyAPI.Exceptions
{
    public class AppException
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string Detail { get; set; }
    }
}
