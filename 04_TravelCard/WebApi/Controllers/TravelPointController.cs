using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Implementations.Mapping;
using Services.Contracts.TravelPoint;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TravelPointController : ControllerBase
    {

        private readonly ITravelPointService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public TravelPointController(ITravelPointService service, ILogger<UserController> logger, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TravelPointModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(_mapper.Map<TravelPointDto,TravelPointModel>(await _service.GetByIdAsync(id)));
        }

        [HttpPost]
        [ProducesResponseType(typeof(TravelPointModel), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync(CreatingTravelPointModel travelpointModel)
        {
            return Ok(await _service.CreateAsync(_mapper.Map<CreatingTravelPointDto>(travelpointModel)));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> EditAsync(int id, UpdatingTravelPointModel travelpointModel)
        {
            await _service.UpdateAsync(id, _mapper.Map<UpdatingTravelPointModel, UpdatingTravelPointDto>(travelpointModel));
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
