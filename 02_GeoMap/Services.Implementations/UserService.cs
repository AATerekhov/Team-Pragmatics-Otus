using Services.Abstractions;
using AutoMapper;
using Services.Repositories.Abstractions;
using Services.Contracts.User;
using Domain.Entities;

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

        public void Create(CreatingUserDto creatingUserDto)
        {
            var user = _mapper.Map<CreatingUserDto, User>(creatingUserDto);
            
            var createdUser = _userRepository.AddAsync(user);
            _userRepository.SaveChanges();
        }
    }
}
