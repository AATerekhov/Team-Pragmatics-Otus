using AutoMapper;
using Domain.Entities;
using GeoMap.Model.Place;
using GeoMap.Model.PlaceType;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.Place;
using Services.Contracts.PlaceType;

namespace GeoMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceController(IPlaceService placeService, IMapper mapper, ILogger<UserController> logger) : ControllerBase
    {
        [HttpGet("{placeTypeId:int}")]
        [ProducesResponseType(typeof(IEnumerable<PlaceModel>), 200)]
        public async Task<IEnumerable<PlaceModel>> GetPlaceForType(int placeTypeId) => (await placeService.GetPlaceForTypeAsync(placeTypeId)).Select(mapper.Map<PlaceModel>);

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PlaceModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(Guid id)
        {
            var place = await placeService.GetByIdAsync(id);
            if (place == null)
                return NotFound();
            return Ok(mapper.Map<PlaceModel>(place));
        }

        [HttpPost]
        [ProducesResponseType(typeof(PlaceModel), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreatingPlaceModel request)
        {
            var place = await placeService.CreatePlaceAsync(mapper.Map<CreatingPlaceDto>(request));
            return CreatedAtAction(nameof(Get), new { id = place.Id }, mapper.Map<PlaceModel>(place));
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatingPlaceModel request)
        {
            if (await placeService.GetByIdAsync(id) == null)
                return NotFound();
            await placeService.UpdateAsync(id, mapper.Map<UpdatingPlaceDto>(request));
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await placeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
