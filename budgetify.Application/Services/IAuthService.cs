using Budgetify.Domain.Entities;

namespace budgetify.Application.Services
{
    public interface IAuthService
    {
        bool VerifyPassword(string hashedPassword, string password);
        string GeneratePassword(string password);
    }
}
