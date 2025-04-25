using Budgetify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budgetify.Application.Services
{
    public class AuthService : IAuthService
    {
        public bool VerifyPassword(string hashedPassword, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public string GeneratePassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
