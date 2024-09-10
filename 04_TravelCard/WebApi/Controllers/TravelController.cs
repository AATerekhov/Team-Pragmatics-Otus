using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Implementations.Mapping;
using Services.Contracts.Travel;
using MassTransit.Futures.Contracts;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TravelController : ControllerBase
    {

        private readonly ITravelService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public TravelController(ITravelService service, ILogger<UserController> logger, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TravelModel>), 200)]
        public async Task<IEnumerable<TravelModel>> GetAll() => (await _service.GetTravelsAsync()).Where(t => !t.IsPrivate).Select(_mapper.Map<TravelModel>);

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(_mapper.Map<TravelDto,TravelModel>(await _service.GetByIdAsync(id)));
        }

        [HttpGet("{userId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<TravelModel>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetOwnTravels(Guid userId)
        {
            var ownTravels = await _service.GetByUserIdAsync(userId);
            if (ownTravels == null)
                return NotFound();
            return Ok(ownTravels.Select(_mapper.Map<TravelModel>));
        }

        [HttpPost]
        [ProducesResponseType(typeof(TravelModel), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAsync(CreatingTravelModel travelModel)
        {
            var travel = await _service.CreateAsync(_mapper.Map<CreatingTravelDto>(travelModel));
            return CreatedAtAction(nameof(Get), new { id = travel.Id }, _mapper.Map<TravelModel>(travel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(int id, UpdatingTravelModel travelModel)
        {
            await _service.UpdateAsync(id, _mapper.Map<UpdatingTravelModel, UpdatingTravelDto>(travelModel));
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
