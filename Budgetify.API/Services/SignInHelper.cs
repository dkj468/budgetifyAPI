using budgetifyAPI.Dtos;
using budgetifyAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace budgetifyAPI.Services
{
    public class SignInHelper : ISignInHelper
    {
        public bool VerifyPassword(User user, string hashedPassword, string password)
        {            
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public string GeneratePassword(User user, string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
