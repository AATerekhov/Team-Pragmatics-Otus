namespace Domain.Entities
{
    /// <summary>
    /// Место на земле
    /// </summary>
    public class Place:IEntity<int>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Тип места.
        /// </summary>
        public virtual PlaceType? PlaceType { get; set; }
        /// <summary>
        /// Географические коордитаны места
        /// </summary>
        public virtual MapPoint? MapPoint { get; set; }

    }
}
