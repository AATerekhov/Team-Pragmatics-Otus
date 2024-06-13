using AutoMapper;
using Services.Contracts.User;

namespace WebApi.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserDto, UserModel>();
            CreateMap<CreatingUserModel, CreatingUserDto>();
            CreateMap<UpdatingUserModel, UpdatingUserDto>();
        }
    }
}
