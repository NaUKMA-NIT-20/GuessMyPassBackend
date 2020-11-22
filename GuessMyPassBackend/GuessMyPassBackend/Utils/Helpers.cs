using MongoDB.Driver;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using System.Linq;

namespace GuessMyPassBackend.Utils
{
    public static class Helpers
    {

        public static string findIdentity(string tokenString)
        {
            try
            {
                JwtSecurityToken token = new JwtSecurityToken(tokenString);

                string id = token.Claims.First(c => c.Type == "id").Value;

                return id;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string generateJwtToken(string id, string JWT_SECRET)
        {

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(JWT_SECRET);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", id) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
