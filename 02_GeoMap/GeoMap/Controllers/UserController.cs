using Microsoft.AspNetCore.Mvc;
using GeoMap.Model.User;
using AutoMapper;
using Services.Abstractions;
using Services.Contracts.User;
using GeoMap.Model.Place;
using Domain.Entities;
using MassTransit.Futures.Contracts;
using Services.Contracts.Place;
using Services.Implementations;

namespace GeoMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService service, ILogger<UserController> logger, IMapper mapper) : ControllerBase
    {       

        [HttpPost]
        [ProducesResponseType(typeof(UserModel), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser([FromBody] CreatingUserModel request)
        {
            await service.CreateAsync(mapper.Map<CreatingUserDto>(request));
            return NoContent();
        }
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PlaceModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await service.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(mapper.Map<UserModel>(user));
        }
    }
}
