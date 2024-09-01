using Services.Abstractions;
using AutoMapper;
using Services.Repositories.Abstractions;
using Services.Contracts.User;
using Domain.Entities;
using Microsoft.Extensions.Hosting;
using MassTransit;
using Services.Contracts.Place;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task CreateAsync(CreatingUserDto creatingUserDto)
        {
            var user = _mapper.Map<CreatingUserDto, User>(creatingUserDto);
            user.Logo = user.Name;
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<User, UserDto>(user);
        }
    }
}
