using Habitica_API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Habitica_API.Helpers
{
    public static class TokenHelper
    {
        public static string GenerateToken(RegisteredUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.User.UserID.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Username));

            var keyBytes = System.Text.Encoding.ASCII.GetBytes("TemplateSecretToVerifyUserTokens");
            var key = new SymmetricSecurityKey(keyBytes);

            var token = tokenHandler.CreateJwtSecurityToken(subject: claimsIdentity, signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return tokenHandler.WriteToken(token);
        }
    }
}
