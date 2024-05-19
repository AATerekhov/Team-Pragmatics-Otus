using AutoMapper;
using Domain.Entities;
using GeoMap.Model.PlaceType;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Services.Contracts.PlaceType;

namespace GeoMap.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaceTypeController(IPlaceTypeService placeTypeService, IMapper mapper, ILogger<UserController> logger) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PlaceTypeModel>), 200)]
        public async Task<IEnumerable<PlaceTypeModel>> GetAll() => (await placeTypeService.GetAllTypesAsync()).Select(mapper.Map<PlaceTypeModel>);

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PlaceTypeModel), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int id)
        {
            var placeType = await placeTypeService.GetByIdAsync(id);
            if (placeType == null)
                return NotFound();
            return Ok(mapper.Map<PlaceTypeModel>(placeType));
        }

        [HttpPost]
        [ProducesResponseType(typeof(PlaceTypeModel), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreatingPlaceTypeModel request)
        {
            var placeType = await placeTypeService.CreatePlaceTypeAsync(mapper.Map<CreatingPlaceTypeDto>(request));
            return CreatedAtAction(nameof(Get), new { id = placeType.Id }, mapper.Map<PlaceTypeModel>(placeType));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatingPlaceTypeModel request)
        {
            if (await placeTypeService.GetByIdAsync(id) == null)
                return NotFound();
            await placeTypeService.UpdateAsync(id, mapper.Map<UpdatingPlaceTypeDto>(request));
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            await placeTypeService.DeleteAsync(id);
            return NoContent();
        }

    }
}
