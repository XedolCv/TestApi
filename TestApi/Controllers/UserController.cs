using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Inter;
using TestApi.Models;

namespace TestApi.Controllers;
[ApiController]
[Authorize]
[Route("[controller]")]


public class UserController : ControllerBase
{

    private readonly IRepository _myRepository;

    public UserController(IRepository myRepository)
    {
        _myRepository = myRepository;
    }
    
    [HttpGet("GET")]
    public JsonResult Get()
    {
        return new JsonResult(_myRepository.GetList<User>());
    }
    [HttpGet( "addnew")]
    public JsonResult Add()
    {
        User user = Models.User.NewRandomUser();
        _myRepository.Create(user);
        _myRepository.Save();
        return new JsonResult(_myRepository.GetList<User>());
    }
}