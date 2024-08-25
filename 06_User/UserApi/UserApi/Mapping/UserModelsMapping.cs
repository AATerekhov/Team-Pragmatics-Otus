using AutoMapper;
using UserApi.DataAccess.BusinessLogic.Models;
using UserApi.Rensposes;
using UserApi.Requests;

namespace UserApi.Mapping
{
    public class UserModelsMapping : Profile
    {
        public UserModelsMapping()
        {
            CreateMap<CreateUserRequest, CreateUserModel>();
            CreateMap<UpdateUserRequest, UserModel>();
            CreateMap<UserModel, UserResponse>();
        }
    }
}
