using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Classes;
using TestApi.Inter;
using TestApi.Models;

namespace TestApi.Controllers;
[ApiController]
[Authorize]
[Route("[controller]")]


public class UserController : ControllerBase
{

    private readonly IRepository _myRepository;
    private readonly IMapper _myMapper;

    public UserController(IRepository myRepository, IMapper myMapper)
    {
        _myRepository = myRepository;
        _myMapper = myMapper;
    }
    
    [HttpGet("GET")]
    public JsonResult Get()
    {
        return new JsonResult(_myRepository.GetList<EFUser>());
    }
    [AllowAnonymous] 
    [HttpGet( "addnew")]
    public JsonResult Add()
    {
        User user = Classes.User.NewRandomUser();
        var tempUser =_myMapper.Map<EFUser>(user);
        _myRepository.Create(tempUser);
        _myRepository.Save();
        return new JsonResult(_myRepository.GetList<EFUser>());
    }
}