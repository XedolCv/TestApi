using TestApi.Classes;

namespace TestApi.Models;

public class AuthResponse
{
    public User User { get; set; }
    public AcessToken token { get; set; }
}