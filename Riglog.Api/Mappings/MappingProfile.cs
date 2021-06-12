using AutoMapper;
using Riglog.Api.Data.Entities;
using Riglog.Api.Models;

namespace Riglog.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();
        }
    }
}