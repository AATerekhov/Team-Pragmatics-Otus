using Microsoft.AspNetCore.Mvc;
using GeoMap.Model.User;
using AutoMapper;
using Services.Abstractions;
using Services.Contracts.User;

namespace GeoMap.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService service,ILogger<UserController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        [HttpPost]
        public void Create(CreatingUserModel userModel)
        {
            _service.Create(_mapper.Map<CreatingUserDto>(userModel));
        }
    }
}
