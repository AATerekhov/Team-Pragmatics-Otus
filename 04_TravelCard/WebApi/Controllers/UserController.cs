using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Implementations.Mapping;
using Services.Contracts.User;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService service, ILogger<UserController> logger, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(_mapper.Map<UserDto, UserModel>(await _service.GetByIdAsync(id)));
        }

        //[HttpGet("{id:guid}")]
        //[ProducesResponseType(typeof(UserModel), 200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //public async Task<IActionResult> Get(int id)
        //{
        //    var user = await _service.GetByIdAsync(id);
        //    if (user == null)
        //        return NotFound();
        //    return Ok(_mapper.Map<UserModel>(user));
        //}

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreatingUserModel userModel)
        {
            return Ok(await _service.CreateAsync(_mapper.Map<CreatingUserDto>(userModel)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(int id, UpdatingUserModel userModel)
        {
            await _service.UpdateAsync(id, _mapper.Map<UpdatingUserModel, UpdatingUserDto>(userModel));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
