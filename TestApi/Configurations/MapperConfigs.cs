using AutoMapper;
using TestApi.Classes;
using TestApi.Models;

namespace TestApi.Configurations;

public class MapperConfigs : Profile
{
    public MapperConfigs()
    {
        CreateMap<User, EFUser>();
        CreateMap< EFUser ,User>();
    }

}