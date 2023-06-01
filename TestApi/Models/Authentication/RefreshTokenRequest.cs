using System.ComponentModel.DataAnnotations.Schema;

namespace TestApi.Models;

[NotMapped]
public class RefreshTokenRequest
{
    public Guid refreshToken { get; set; }
    public AcessToken acessToken { get; set; }
} 
