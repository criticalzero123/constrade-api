using ConstradeApi.Entity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConstradeApi.Services.Jwt
{
    public class JwtAuthentication 
    {
        private readonly IConfiguration configuration;
        private string JwtKey { get; set; }

        public JwtAuthentication()
        {
            configuration = new ConfigurationBuilder().AddUserSecrets<JwtAuthentication>().Build();

            JwtKey = configuration["Jwt:Key"]!;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Token Generated</returns>
        public static string CreateToken(string email, int userId)
        {
            JwtAuthentication authentication = new JwtAuthentication();

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authentication.JwtKey));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    //if you want to use email as a name identifier and this is recommended in the documentation
                    //new Claim(JwtRegisteredClaimNames.Sub, email),
                    //new Claim(ClaimTypes.NameIdentifier, email),

                    new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            
            return jwtToken;
        }
    }
}
