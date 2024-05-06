using AutoMapper;
using Services.Contracts.User;
using GeoMap.Model.User;

namespace GeoMap.Mapping
{
    /// <summary>
    /// Профиль автомаппера для сущности пользователя.
    /// </summary>
    public class UserMappingsProfile: Profile
    {
        public UserMappingsProfile()
        {
            CreateMap< UserDto, UserModel> ();
            CreateMap<CreatingUserModel, CreatingUserDto>();
        }
    }
}
