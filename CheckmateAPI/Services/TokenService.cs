using Checkmate.DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CheckmateAPI.Services
{
    public class TokenConfig
    {
        public string Signature { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
    }

    public class TokenService
    {
        private readonly TokenConfig _config;

        public TokenService(TokenConfig config)
        {
            _config = config;
        }

        public string CreateToken(Member member)
        {
            JwtSecurityToken token = new JwtSecurityToken(
                _config.Issuer,
                null,
                CreateClaims(member),
                DateTime.Now,
                null,
                CreateCredentials()
            );
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);
        }

        private SigningCredentials CreateCredentials()
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Signature)),
                SecurityAlgorithms.HmacSha256
            );
        }

        private IEnumerable<Claim> CreateClaims(Member member)
        {
            yield return new Claim(ClaimTypes.Role, member.Role.ToString());
            yield return new Claim(ClaimTypes.NameIdentifier, member.Id.ToString(), ClaimValueTypes.Integer);
        }
    }
}
