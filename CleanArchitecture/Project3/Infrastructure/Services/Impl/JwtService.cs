using Infrastructure.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Impl
{
    public class JwtService : IJwtService
    {
        private readonly JwtConfig _jwtConfig;
        public readonly UserManager<IdentityUser> _userManager;
        public JwtService(IOptionsMonitor<JwtConfig> optionsMonitor, UserManager<IdentityUser> userManager)
        {
            _jwtConfig = optionsMonitor.CurrentValue;
            _userManager = userManager;
        }
        public async Task<string> GenerateJwtTokenAsync(IdentityUser user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault();

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };
            if (!string.IsNullOrEmpty(userRole))
            {
                tokenDescription.Subject.AddClaim(new Claim(ClaimTypes.Role, userRole));
            }
            else
            {
                tokenDescription.Subject.AddClaim(new Claim(ClaimTypes.Role, "User"));
            }

            var token = jwtHandler.CreateToken(tokenDescription);
            var jwtToken = jwtHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
