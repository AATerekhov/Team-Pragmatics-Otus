﻿using Services.Abstractions;
using AutoMapper;
using Services.Repositories.Abstractions;
using Services.Contracts.User;
using Domain.Entities;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        public readonly IMapper _mapper;
        public readonly IUserRepository _userRepository;
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public void Create(CreatingUserDto creatingUserDto)
        {
            var user = _mapper.Map<CreatingUserDto, User>(creatingUserDto);
            
            var createdUser = _userRepository.AddAsync(user);
            _userRepository.SaveChanges();

            //TODO: Тут идет логика работы с createdUser, который сохранен в БД.
        }
    }
}
