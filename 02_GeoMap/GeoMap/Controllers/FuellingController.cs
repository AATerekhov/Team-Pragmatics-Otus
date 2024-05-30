using AutoMapper;
using GeoMap.Model.Fuelling;
using GeoMap.Model.Place;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.Fuelling;
using Services.Contracts.Place;
using Services.Implementations;

namespace GeoMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuellingController(IFuellingService fuellingService, IMapper mapper, ILogger<UserController> logger) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FuellingModel>), 200)]
        public async Task<IEnumerable<FuellingModel>> GetAll(int placeTypeId) => (await fuellingService.GetAllFuellingsAsync()).Select(mapper.Map<FuellingModel>);

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PlaceModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid id)
        {
            var fuelling = await fuellingService.GetByIdAsync(id);
            if (fuelling == null)
                return NotFound();
            return Ok(mapper.Map<PlaceModel>(fuelling));
        }

        [HttpPost]
        [ProducesResponseType(typeof(PlaceModel), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreatingFuellingModel request)
        {
            var fuelling = await fuellingService.CreateFuellingAsync(mapper.Map<CreatingFuellingDto>(request));
            return CreatedAtAction(nameof(Get), new { id = fuelling.Id }, mapper.Map<FuellingModel>(fuelling));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatingFuellingModel request)
        {
            if (await fuellingService.GetByIdAsync(id) == null)
                return NotFound();
            await fuellingService.UpdateAsync(id, mapper.Map<UpdatingFuellingDto>(request));
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await fuellingService.DeleteAsync(id);
            return NoContent();
        }
    }
}
