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
        var token = AcessToken.GenerateNewToken(user);
        if (token == null)
        {
            return BadRequest(new { errorText = "Invalid username or password." });
        }
        return Ok(token);
    }

    [HttpPost("/refreshToken")]
    public IActionResult RefreshToken(RefreshTokenRequest request)
    {
        try
        {
            TokenData date = new TokenData(request.acessToken,_rep,request.refreshToken);
            if (date.refreshAvailability)
            {
                AcessToken response = AcessToken.GenerateNewToken(date.tokenOwner);
                date.tokenOwner.GenerateNewRefresh(_rep);
                return Ok(response);
            }
            else
            {
                return Unauthorized();
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
            _rep.Dispose();
        }
       
    }
}