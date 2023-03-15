namespace TestApi.Models;

public class RefreshTokenRequest
{
    public Guid refreshToken { get; set; }
    public string acessToken { get; set; }
    //public  Type { get; set; }
    
}