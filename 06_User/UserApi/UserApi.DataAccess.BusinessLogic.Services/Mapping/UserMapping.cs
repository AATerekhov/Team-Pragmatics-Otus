using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.DataAccess.Entities;

namespace UserApi.DataAccess.BusinessLogic.Models.Mapping
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
