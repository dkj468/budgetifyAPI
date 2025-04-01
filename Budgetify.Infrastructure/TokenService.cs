using budgetify.Application.Services;
using Budgetify.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Budgetify.Infrastructure
{
    public class TokenService : ITokenService
    {
        string _jwtTokenKey;
        public TokenService(string jwtTokenKey)
        {
            _jwtTokenKey = jwtTokenKey;
        }
        public string CreateToken(User user)
        {
            // Create claims based on user properties 
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.Name, user.DisplayName),
                new Claim (ClaimTypes.Email, user.Email),
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            // get the key and signing credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // token description
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = cred
            };


            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
