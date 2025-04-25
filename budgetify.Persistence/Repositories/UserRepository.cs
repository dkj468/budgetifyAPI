
using budgetify.Application.Repositories;
using budgetify.Persistence.Contexts;
using Budgetify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace budgetify.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _ctx;
        private User _User;
        public UserRepository(DataContext ctx)
        {
            _ctx = ctx;
        }

        public User User { get => _User; set => _User = value; }

        public async Task CreateNewUser(User user)
        {
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<User> GetUserById(int Id)
        {
            var user = await _ctx.Users.FindAsync(Id);
            return user;
        }
    }
}
