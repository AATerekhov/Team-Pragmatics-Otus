using AutoMapper;
using Domain.Entities;
using Services.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Mapping
{
    public class UserMappingsProfile:Profile
    {
        public UserMappingsProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<CreatingUserDto, User>();
            //.ForMember(d=>d.Id, map => map.Ignore())
            //.ForMember(d => d.Deleted, map => map.Ignore())
            //.ForMember(d => d.Name, map => map.Ignore())
            //.ForMember(d => d.Surname, map => map.Ignore());

            CreateMap<UserDto, User> ();
        }
    }
}
