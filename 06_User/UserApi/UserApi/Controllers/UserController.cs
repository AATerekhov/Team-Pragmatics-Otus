using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserApi.DataAccess.BusinessLogic.Models;
using UserApi.DataAccess.BusinessLogic.Services.Base;
using UserApi.Rensposes;
using UserApi.Requests;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService, IMapper mapper): ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), 200)]
        public async Task<IEnumerable<UserResponse>> GetAll() => (await userService.GetUsersAsync()).Select(mapper.Map<UserResponse>);

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await userService.GetUserAsync(id);
            if (user == null)
                return NotFound();
            return Ok(mapper.Map<UserResponse>(user));
        }

        [HttpPost("Authorization")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Authorization([FromBody] GetUserRequest request)
        {
            var users = (await userService.GetUsersAsync()).Select(mapper.Map<UserResponse>);
            var user = users.Where(u => u.Name == request.Name && u.Password == request.Password);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            var user = await userService.CreateUserAsync(mapper.Map<CreateUserModel>(request));
            return CreatedAtAction(nameof(Get), new { id = user.Id }, mapper.Map<UserResponse>(user));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
        {
            if (await userService.GetUserAsync(id) == null)
                return NotFound();
            await userService.UpdateUserAsync(mapper.Map<UserModel>(request));
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
