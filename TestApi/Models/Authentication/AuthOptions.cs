using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TestApi.Classes;


namespace TestApi.Models;
[NotMapped]
public class AuthOptions
{
    public const string issuer = "mytestapi"; // издатель токена
    public const string audiendce = "client"; // потребитель токена
    protected const string key = "uE27aB[sHtTq{^cu7z??2jdCQsC$3m";   // ключ для шифрации
    public const int lifetime = 5; // время жизни токена - 1 минута
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
    }

    /*public IActionResult GetAcessToken(User user)
    {
        var identity = AuthOptions.GetIdentity(user);
        if (identity == null)
        {
            return BadRequest(new { errorText = "Invalid username or password." });
        } 
        
        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.issuer,
            audience: AuthOptions.audiendce,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.lifetime)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
 
        var response = new
        {
            access_token = encodedJwt,
            username = identity.Name
        };
    }*/

    public static ClaimsIdentity GetIdentity(User person)
    {
        if (person != null)
        {
            var claims = new List<Claim>
            {
                new Claim("id",person.id.ToString())
            };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
 
        // если пользователя не найдено
        return null;
    }
}