using Microsoft.IdentityModel.Tokens;
using PumoxWebApplication.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PumoxWebApplication.Repositories
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _jwtSettings;
        public JwtHandler(IConfiguration configuration)
        {
            _jwtSettings = new JwtSettings
            {
                ExpiryInMinutes = int.Parse(configuration.GetSection("Jwt:ExpiryInMinutes").Value),
                SigningKey = configuration.GetSection("Jwt:SigningKey").Value,
                Site = configuration.GetSection("Jwt:Site").Value
            };

        }

        public string CreateToken(string username, string password)
        {
            DateTime now = DateTime.UtcNow;
            Claim[] claims = new Claim[]
            {
                new Claim("Username", username),
                new Claim("Password", password)
            };

            DateTime expires = now.AddMinutes(_jwtSettings.ExpiryInMinutes);
            SigningCredentials signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey)),
                SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Site,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
                );

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
    }
}
