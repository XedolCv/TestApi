using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using TestApi.Inter;

namespace TestApi.Models;

public class User :IEntity
{public Guid id { get; set; }
    public string userName { get; set; }
    public string login { get; set; }
    public string password { get; set; }
    public string userRole { get; set; }
    [NotMapped]
    public string test { get; set; }

    private Guid? refreshToken { get; set; }
    private DateTime? expirationRefreshTokenTime { get; set; } 

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

    public bool CheckRefresh(Guid refToken)
    {
        return refreshToken == refToken && expirationRefreshTokenTime >= DateTime.Now;
    }

    public bool GenerateNewRefresh(IRepository _rep)
    {
        if (this.refreshToken is null || !LifetimeRefreshValidator(this))
        {
            this.refreshToken = new Guid?();
            this.expirationRefreshTokenTime = DateTime.Now.AddDays(1);
            _rep.Update<User>(this);
            _rep.Save();
            return true;
        }
        return false;
    }
    public void CreateRefresh(IRepository _rep)
    {
        this.refreshToken = new Guid?();
            this.expirationRefreshTokenTime = DateTime.Now.AddDays(1);
            _rep.Update<User>(this);
            _rep.Save();
    }

    private static bool LifetimeRefreshValidator(User user)
    {
        return user.expirationRefreshTokenTime >= DateTime.Now;
    }

    public static User NewRandomUser()
    {
        return (new User(Guid.NewGuid(),"name"+ new Random().Next(0,100),"login"+ new Random().Next(0,100),"huy123","Антон"));
    }

    

}
