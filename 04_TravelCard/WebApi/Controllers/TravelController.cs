using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Implementations.Mapping;
using Services.Contracts.Travel;

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
        public async Task<IEnumerable<TravelModel>> GetAll() => (await _service.GetTravelsAsync()).Select(_mapper.Map<TravelModel>);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return Ok(_mapper.Map<TravelDto,TravelModel>(await _service.GetByIdAsync(id)));
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
        public async Task<IActionResult> CreateAsync(CreatingTravelModel request)
        {
            var travel = await _service.CreateAsync(_mapper.Map<CreatingTravelDto>(request));
            if (travel != 0)
            {
                return StatusCode(200, $"{travel}");
            }
            else return StatusCode(400, "Invalid data!");           
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(int id, UpdatingTravelModel travelModel)
        {
            if(await _service.GetByIdAsync(id) == null)
                return NotFound();
            await _service.UpdateAsync(id, _mapper.Map<UpdatingTravelModel, UpdatingTravelDto>(travelModel));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
