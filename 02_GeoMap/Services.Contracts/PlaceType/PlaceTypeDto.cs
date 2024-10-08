﻿namespace Services.Contracts.PlaceType
{
    public class PlaceTypeDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }
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
