using ApiRestMinimal.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace ApiRestMinimal.Custom
{
    public class Utility
    {
        private readonly IConfiguration _configuration;

        public Utility(in IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public string GenerateJWT( User user)
        {
         var userClaims = new[]
         {
             new Claim(ClaimTypes.NameIdentifier , user.Id.ToString() ),
             new Claim(ClaimTypes.Email, user.Email ),
             
         };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings : SecretKey"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            //deatelles tokens
            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires:DateTime.UtcNow.AddMinutes(10),
                signingCredentials:credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);
        }
    }
}
