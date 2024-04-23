using TracingProgmatica.Models.MapPoint;
using TracingProgmatica.Models.PlaceType;

namespace TracingProgmatica.Models.Place
{
    public class PlaceModel
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Тип места.
        /// </summary>
        public  PlaceTypeModel? PlaceType { get; set; }
        /// <summary>
        /// Географические коордитаны места
        /// </summary>
        public MapPointMadel? MapPoint { get; set; }
    }
}
