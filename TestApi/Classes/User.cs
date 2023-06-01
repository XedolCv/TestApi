using System.ComponentModel.DataAnnotations.Schema;
using TestApi.Inter;
using TestApi.Models;

namespace TestApi.Classes;

[NotMapped]
public class User
{
    public Guid id { get; set; }
    public string userName { get; set; }
    public string login { get; set; }
    public string password { get; set; }
    public string userRole { get; set; }
    public Guid? refreshToken { get;  set; }
    public DateTime? expirationRefreshTokenTime { get; set; } 
    public User()
    {
        
    }

    public User(string login, string password, string userRole)
    {
        this.login = login;
        this.password = password;
        this.userRole = userRole;
    }

    public User(Guid id, string userName, string login, string password, string userRole)
    {
        this.id = id;
        this.userName = userName;
        this.login = login;
        this.password = password;
        this.userRole = userRole;
    }
    public static bool LifetimeRefreshValidator(User user)
    {
        return user.expirationRefreshTokenTime >= DateTime.Now;
    }
    public bool CheckRefresh(Guid refToken)
    {
        return refreshToken == refToken && expirationRefreshTokenTime >= DateTime.Now;
    }
    public static User NewRandomUser()
    {
        return (new User(Guid.NewGuid(),"name"+ new Random().Next(0,100),"login"+ new Random().Next(0,100),"pass123","SimpleUser"));
    }
   
}