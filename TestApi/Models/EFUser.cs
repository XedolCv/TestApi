using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using TestApi.Inter;

namespace TestApi.Models;

public class EFUser :IEntity
{
    public Guid id { get; set; }
    public string userName { get; set; }
    public string login { get; set; }
    public string password { get; set; }
    public string userRole { get; set; }
    public Guid? refreshToken { get; private set; }
    public DateTime? expirationRefreshTokenTime { get; set; } 

   

    

}
