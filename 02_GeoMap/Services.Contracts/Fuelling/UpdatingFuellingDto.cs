﻿namespace Services.Contracts.Fuelling
{
    public class UpdatingFuellingDto
    {
        /// <summary>
        /// Название локации
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// На заправке есть кафе.
        /// </summary>
        public bool IsCafe { get; set; }
        /// <summary>
        /// На заправке есть магазин продуктов.
        /// </summary>
        public bool IsFoodShop { get; set; }
    }
}
