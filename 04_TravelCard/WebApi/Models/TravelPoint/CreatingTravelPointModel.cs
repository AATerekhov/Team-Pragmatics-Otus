namespace Services.Contracts.TravelPoint
{
    /// <summary>
    /// Модель создания точки остановки
    /// </summary>
    public class CreatingTravelPointModel
    {
        /// <summary>
        /// Идентификатор путешествия.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Описание путешествия
        /// </summary>
        public string TravelDesc { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }

    }
}
