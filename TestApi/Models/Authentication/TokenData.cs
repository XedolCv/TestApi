using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Tokens.Jwt;
using TestApi.Inter;

namespace TestApi.Models;

[NotMapped]
public class TokenData
{
    public Guid userId { get; set; }
    private DateTime expDateTime { get; set; }
    public bool isValid { get; set; }
    public bool isExpired { get; set; }
    public bool refreshAvailability { get; set; }
    public User tokenOwner { get; private set; }

    public TokenData(string accesToken, IRepository rep,Guid? refreshToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(accesToken);
        var cc = jwtSecurityToken.Claims;
        Guid id = new Guid();
        bool res = Guid.TryParse(jwtSecurityToken.Claims.First(c => c.Type == "id").Value, out id);
        if (res)
        {
            userId = id;
            tokenOwner = rep.GetOne<User>(it => it.id == id);
        }
        long ticks = long.Parse(jwtSecurityToken.Claims.First(c => c.Type == "exp").Value);
        expDateTime = new DateTime(1970,1,1,0,0,0).AddSeconds(ticks);
        isExpired = expDateTime < DateTime.UtcNow;
        if (refreshToken != null)
        {
            if (tokenOwner.CheckRefresh((Guid)refreshToken) && isExpired && isValid)
            {
                refreshAvailability = true;
            }
            else
            {
                refreshAvailability = true;
            }
        }
    }
}
