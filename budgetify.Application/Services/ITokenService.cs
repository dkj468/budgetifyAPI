using Budgetify.Domain.Entities;

namespace budgetify.Application.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
