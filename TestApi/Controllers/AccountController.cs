using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TestApi.Inter;
using TestApi.Models;

namespace TestApi.Controllers;
[ApiController]

[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IRepository _rep;

    public AccountController(IRepository rep)
    {
        _rep = rep;
    }

    [AllowAnonymous]
    [HttpPost("/token")]
    public IActionResult Token(string username, string password)
    {
        User user = _rep.GetOne<User>(it => it.login == username && it.password == password);
        user.CreateRefresh(_rep);
        var identity = AuthOptions.GetIdentity(user);
        if (identity == null)
        {
            return BadRequest(new { errorText = "Invalid username or password." });
        }
        AcessToken response = AcessToken.GenerateNewToken(identity,user);
        return Ok(response);
    }
    
    [HttpPost("/refreshToken")]
    public IActionResult RefreshToken(RefreshTokenRequest request)
    {
        var token = "[encoded jwt]";  
        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(request.acessToken);
        
        //sss
         //request.acessToken
        // User user = _rep.GetOne<User>();
        // user.CreateRefresh(_rep);
        // var identity = AuthOptions.GetIdentity(user);
        // if (identity == null)
        // {
        //     return BadRequest(new { errorText = "Invalid username or password." });
        // }
        // AcessToken response = AcessToken.GenerateNewToken(identity,user);
        return Ok(true);
    }
}