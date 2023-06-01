using AutoMapper;
using TestApi.Classes;
using TestApi.Inter;
using TestApi.Models;

namespace TestApi.Services;

public class AuthService: IAuthService
{
    private readonly IRepository _rep;
    private readonly IMapper _myMapper;

    public AuthService(IRepository myRepository, IMapper myMapper)
    {
        _rep = myRepository;
        _myMapper = myMapper;
    }
    public  bool GenerateNewRefresh(User user) //TODO
    {
         if (user.refreshToken is null || ! User.LifetimeRefreshValidator(user))
        {
            user.refreshToken = new Guid?();
            user.expirationRefreshTokenTime = DateTime.UtcNow.AddDays(1);
            _rep.Update<EFUser>(_myMapper.Map<EFUser>(user));
            _rep.Save();
            return true;
        }
        return false;
    }
    public  void CreateRefresh(User user)
    {
        user.refreshToken = Guid.NewGuid();
        user.expirationRefreshTokenTime = DateTime.UtcNow.AddDays(1);
        var ss = _myMapper.Map<EFUser>(user);
        _rep.Update<EFUser>(ss);
        _rep.Save();
    }

    public AuthResponse Auth(string login, string password)
    {
        AuthResponse response = new AuthResponse();  
        response.User = _myMapper.Map<User>(_rep.GetOne<EFUser>(it => it.login == login && it.password == password));
        if (response.User.refreshToken != null && response.User.refreshToken != new Guid())
        {
            if (!response.User.CheckRefresh((Guid)response.User.refreshToken))
            {
                GenerateNewRefresh(response.User);
            }
        }
        else
        {
            CreateRefresh(response.User);
        }
        response.token = AcessToken.GenerateNewToken(response.User);
        return response;
    }

    public RefreshTokenRequest RefreshToken(RefreshTokenRequest request)
    {
        TokenData date = new TokenData(request.acessToken,_rep,_myMapper,request.refreshToken);
        if (date.refreshAvailability)
        {
            AcessToken response = AcessToken.GenerateNewToken(date.tokenOwner);
            GenerateNewRefresh(date.tokenOwner);
            RefreshTokenRequest rtr = new RefreshTokenRequest();
            rtr.acessToken = response;
            rtr.refreshToken = (Guid)date.tokenOwner.refreshToken;
            return rtr;
        }
        else
        {
            return null;
        }
    }
}