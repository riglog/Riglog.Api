using AutoMapper;
using Riglog.Api.Data.Sql.Entities;
using Riglog.Api.Services.Models;

namespace Riglog.Api.Services.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserModel, User>();
        CreateMap<User, UserModel>();
    }
}