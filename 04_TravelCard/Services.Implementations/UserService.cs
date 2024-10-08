﻿using AutoMapper;
using Services.Abstractions;
using Services.Repositories.Abstractions;
using Services.Contracts.User;
using Domain.Entities;
using Infrastructure.Repositories.Implementations;
using System.Diagnostics;

namespace Services.Implementations
{
    /// <summary>
    /// Сервис работы с юзерами.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _UserRepository;

        public UserService (IMapper mapper, IUserRepository UserRepository)
        {
            _mapper = mapper;
            _UserRepository = UserRepository;
        }

        /// <summary>
        /// Создать юзера.
        /// </summary>
        /// <param name="CreatingUserDto"> ДТО юзера. </param>
        /// <returns> Идентификатор. </returns>
        public async Task<Guid> CreateAsync(CreatingUserDto creatingUserDto)
        {
            var UserEntity = _mapper.Map<CreatingUserDto, User>(creatingUserDto);
            UserEntity.Deleted = false;
            var createdUser = await _UserRepository.AddAsync(UserEntity);
            await _UserRepository.SaveChangesAsync();
            return createdUser.Id;

            //Брокер
            //await _busControl.Publish(new MessageDto
            //{
            //    Content = $"User {createdUser.Id} with subject {createdUser.Subject} is added"
            //});

            return createdUser.Id;
        }

        /// <summary>
        /// Удалить юзера.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        public async Task DeleteAsync(Guid id)
        {
            var user = await _UserRepository.GetAsync(id, CancellationToken.None);
            user.Deleted = true;
            _UserRepository.Update(user);
            await _UserRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Получить юзера.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО юзера. </returns>
        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            //return _mapper.Map<UserDto>(await _UserRepository.GetAsync(id, CancellationToken.None));
            var user = await _UserRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<User, UserDto>(user);
        }


        /// <summary>
        /// Получить все путешествия.
        /// </summary>
        /// <returns> IEnumerable путешествий. </returns>
        public async Task<IEnumerable<UserDto>> GetUsersAsync() => (await _UserRepository.GetAllAsync()).Select(_mapper.Map<UserDto>);

        /// <summary>
        /// Изменить юзера.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updatingUserDto"> ДТО юзера. </param>
        public async Task UpdateAsync(Guid id, UpdatingUserDto updatingUserDto)
        {
            var user = await _UserRepository.GetAsync(id, CancellationToken.None);
            if (user == null)
            {
                throw new Exception($"Юзер с id = {id} не найден");
            }

            user.Email = updatingUserDto.Email;
            _UserRepository.Update(user);
            await _UserRepository.SaveChangesAsync();
        }
    }
}
