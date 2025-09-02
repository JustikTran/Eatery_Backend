using Eatery_API.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Eatery_API.Helpers.Authentication
{
    public class TokenGenerator
    {
        private static readonly Lazy<TokenGenerator> lazy =
            new Lazy<TokenGenerator>(() => new TokenGenerator());
        public static TokenGenerator Instance { get { return lazy.Value; } }

        private readonly IConfiguration _configuration;
        private TokenGenerator()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
            _configuration = builder.Build();
        }

        public string GetToken(Account data)
        {
            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var expired = _configuration.GetValue<int>("JwtConfig:Expired");
            var tokenExpireTimestamp = DateTime.UtcNow.AddDays(expired);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, data.Id.ToString()),
                    new Claim(ClaimTypes.Name, data.Username),
                    new Claim(ClaimTypes.Role, data.Role),
                    new Claim(ClaimTypes.Email, data.Email),
                }),
                Expires = tokenExpireTimestamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return accessToken!;

        }
    }
}
