using AutoMapper;
using UserApi.DataAccess.BusinessLogic.Models;
using UserApi.DataAccess.Entities;

namespace UserApi.DataAccess.BusinessLogic.Services.Mapping
{
    public class UserMapping:  Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserModel>();
            CreateMap<CreateUserModel, User>();
            CreateMap<UserModel, User>();
        }
    }
}
