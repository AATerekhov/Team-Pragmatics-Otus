using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Классификатор мест на земле в рамках Сервиса.
    /// </summary>
    public class PlaceType:IEntity<int>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }
        public string? Name { get; set; }

    }
}
