using AutoMapper;
using Domain.Entities;
using Services.Contracts.User;

namespace Services.Implementations.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности юзера.
    /// </summary>
    public class UserMappingProfileDto : Profile
    {
        public UserMappingProfileDto()
        {
            CreateMap<User, UserDto>();

            CreateMap<CreatingUserDto, User>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.Deleted, map => map.Ignore())
                .ForMember(d => d.Login, map => map.Ignore());

            CreateMap<UpdatingUserDto, User>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.Deleted, map => map.Ignore())
                .ForMember(d => d.Login, map => map.Ignore());
        }
    }
}
