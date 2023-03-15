using System.ComponentModel.DataAnnotations.Schema;
using TestApi.Inter;

namespace TestApi.Models;


[NotMapped]
public class Services : IEntity<int>
{
    public int id { get; set; }

    public string name { get; set; }

}