using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.API.Repositories.Interfaces;

namespace Web.API.Repositories.Classes
{
    public class tokenRepository : ItokenRepository
    {
        private readonly IConfiguration IConfiguration;

        public tokenRepository(IConfiguration IConfiguration)
        {
            this.IConfiguration = IConfiguration;
        }
        public string GenerateTokenAsync(IdentityUser User, string[] roles)
        { 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.UserName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();   
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IConfiguration["Jwt:Key"])); 
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                IConfiguration["Jwt:Issuer"],
                IConfiguration["Jwt:Audience"], 
                claims, 
                expires: DateTime.UtcNow.AddHours(1), 
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
