﻿using System.Security.Claims;
using TestApi.Inter;

namespace TestApi.Models;

public class User :IEntity
{public Guid id { get; set; }
    public string userName { get; set; }
    public string login { get; set; }
    public string password { get; set; }
    public string userRole { get; set; }

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

    public bool GenerateNewRefresh()
    {
        if (this.refreshToken is null || !LifetimeRefreshValidator(this))
        {
            this.refreshToken = new Guid?();
            this.expirationRefreshTokenTime = DateTime.Now.AddDays(1);
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
