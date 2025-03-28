using budgetifyAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace budgetifyAPI.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config) 
        { 
            _config = config;
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
            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes(_config["JwtTokenKey"]));
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

        public ClaimsPrincipal ValidateJwtToken(string token)
        {
            // JWT secret
            var key = Encoding.UTF8.GetBytes(_config["JwtTokenKey"]);
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var claimsPrenciple = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                    ValidateIssuer = false,

                },
                out SecurityToken validatedToken);

                return claimsPrenciple;
            }
            catch (SecurityTokenExpiredException ex)
            {
                throw new ApplicationException("Token has expired");
            }
        }

    }
}
