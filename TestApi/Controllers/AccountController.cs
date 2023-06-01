using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApi.Inter;
using TestApi.Models;
using TestApi.Services;

namespace TestApi.Controllers;
[ApiController]

[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IRepository _rep;
    private readonly IAuthService _authService;

    public AccountController(IRepository rep, IAuthService authService)
    {
        _rep = rep;
        _authService = authService;
    }
    
    [AllowAnonymous]
    [HttpGet("/auth")]
    public IActionResult AuthorizeUser(string username, string password)
    {
        var response = _authService.Auth(username, password);
        if (response.token == null)
        {
            return BadRequest(new { errorText = "Invalid username or password." });
        }
        return Ok(response);
    }

    [HttpPost("/refreshToken")]
    public IActionResult RefreshToken(RefreshTokenRequest request)
    {
        try
        {
            var req = _authService.RefreshToken(request);
            if (req is not null)
            {
                return Ok(req);
            }
            else
            {
                return Unauthorized();
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}