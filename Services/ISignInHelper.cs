using budgetifyAPI.Dtos;
using budgetifyAPI.Models;

namespace budgetifyAPI.Services
{
    public interface ISignInHelper
    {
        bool VerifyPassword(User user, string hashedPassword, string password);
        string GeneratePassword(User user, string password);
    }
}
