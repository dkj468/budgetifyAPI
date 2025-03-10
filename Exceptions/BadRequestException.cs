using System.Net;

namespace budgetifyAPI.Exceptions
{
    public class BadRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public BadRequestException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
