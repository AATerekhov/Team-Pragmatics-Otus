﻿using AutoMapper;
using CommonNamespace;
using MassTransit;
using Newtonsoft.Json;
using System.Net.Http.Json;
using UserApi.DataAccess.BusinessLogic.Models;
using UserApi.DataAccess.BusinessLogic.Services.Base;
using UserApi.DataAccess.BusinessLogic.Services.Exceptions;
using UserApi.DataAccess.Entities;
using UserApi.DataAccess.Repositories.Abstractions;

namespace UserApi.DataAccess.BusinessLogic.Services
{
    public class UserService(IUserRepository usersRepository, IMapper mapper, IBusControl _busControl) : IUserService
    {
        public async Task<IEnumerable<UserModel>> GetUsersAsync() => (await usersRepository.GetAllAsync()).Select(mapper.Map<UserModel>);
        public async Task<UserModel?> GetUserAsync(Guid id)
        {
            var userResult = mapper.Map<UserModel>(await usersRepository.GetByIdAsync(id));
            return userResult;
        }
        public async   Task<UserModel> CreateUserAsync(CreateUserModel user)
        {
            var userEntity = mapper.Map<User>(user);
            var userResult = await usersRepository.AddAsync(userEntity);

            await _busControl.Publish(new MessageCreateUserDto
            {
                Content = JsonConvert.SerializeObject(userResult),
            });

            return mapper.Map<UserModel>(userResult);
        }
        public async Task UpdateUserAsync(UserModel user)
        {
            var userEntity = mapper.Map<User>(user);
            var foundUser = await usersRepository.GetByIdAsync(user.Id);
            if (foundUser == null)
                throw new UserNotExistException(user.Id);
            await usersRepository.UpdateAsync(userEntity);
        }
        public async Task DeleteUserAsync(Guid id)
        {
            var user = await usersRepository.GetByIdAsync(id);
            if (user == null)
                throw new UserNotExistException(id);
            await usersRepository.DeleteAsync(user);
        }
    }
}
