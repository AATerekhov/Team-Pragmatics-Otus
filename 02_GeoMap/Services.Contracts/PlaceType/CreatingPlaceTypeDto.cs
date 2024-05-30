using System;
namespace Services.Contracts.PlaceType
{
    public class CreatingPlaceTypeDto
    {
        /// <summary>
        /// Название типа.
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Описание.
        /// </summary>
        public string? Description { get; set; }
    }
}
