using BGMaterial.Application.Dtos;
using BGMaterial.Application.Features.Mediator.Results;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BGMaterial.Application.Tools
{
    public class JwtTokenGenerator
    {
        public static TokenResponseDto GenerateToken(GetCheckAppUserQueryResult result)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()));

            if (!string.IsNullOrWhiteSpace(result.Username))
                claims.Add(new Claim("Username", result.Username));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);

            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtTokenDefaults.ValidIssuer, audience: JwtTokenDefaults.ValidAudience, claims: claims, notBefore: DateTime.UtcNow, expires: expireDate, signingCredentials: signingCredentials);


            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return new TokenResponseDto(tokenHandler.WriteToken(token), expireDate);
        }
    }
}
