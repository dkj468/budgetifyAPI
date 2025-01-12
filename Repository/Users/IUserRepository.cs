using budgetifyAPI.Models;

namespace budgetifyAPI.Repository.Users
{
    public interface IUserRepository
    {
        User User { get; set; }
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(int Id);
        Task CreateNewUser(User user);
    }
}
