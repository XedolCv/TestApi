using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace TestApi.Models;

[NotMapped]
public class AcessToken
{
    public string access_token { get; set; }
    //public Guid userId { get; set; }

    public static AcessToken GenerateNewToken(User user)
    {
        var ident = AuthOptions.GetIdentity(user);
        if (ident != null)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.issuer,
                audience: AuthOptions.audiendce,
                notBefore: now,
                claims: ident.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
            AcessToken response = new AcessToken
            {
                access_token = encodedJwt,
                //userId = user.id
            };
            return response; 
        }
        else
        {
            return null;
        }


    }
}