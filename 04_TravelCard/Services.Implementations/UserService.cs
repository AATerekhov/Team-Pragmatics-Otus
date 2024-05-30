using AutoMapper;
using Services.Abstractions;
using Services.Repositories.Abstractions;
using Services.Contracts.User;
using Domain.Entities;

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
        public async Task<int> CreateAsync(CreatingUserDto creatingUserDto)
        {
            var UserEntity = _mapper.Map<CreatingUserDto, User>(creatingUserDto);
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
        public async Task DeleteAsync(int id)
        {
            var user = await _UserRepository.GetAsync(id, CancellationToken.None);
            user.Deleted = true;
            await _UserRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Получить юзера.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО юзера. </returns>
        public async Task<UserDto> GetByIdAsync(int id)
        {
            //return _mapper.Map<UserDto>(await _UserRepository.GetAsync(id, CancellationToken.None));
            var user = await _UserRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<User, UserDto>(user);
        }

        //public async Task<UserDto?> GetUserAsync(int id) => _mapper.Map<UserDto>(await _UserRepository.GetUserByIdAsync(id));

        /// <summary>
        /// Изменить юзера.
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="updatingUserDto"> ДТО юзера. </param>
        public async Task UpdateAsync(int id, UpdatingUserDto updatingUserDto)
        {
            var user = await _UserRepository.GetAsync(id, CancellationToken.None);
            if (user == null)
            {
                throw new Exception($"Юзер с id = {id} не найден");
            }

            user.Login = updatingUserDto.Login;
            _UserRepository.Update(user);
            await _UserRepository.SaveChangesAsync();
        }
    }
}
