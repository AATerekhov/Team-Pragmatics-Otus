using AutoMapper;
using Services.Contracts.User;

namespace WebApi.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserDto, UserModel>();
            CreateMap<CreatingUserDto, CreatingUserModel>();
            CreateMap<UpdatingUserDto, UpdatingUserModel>();
        }
    }
}
