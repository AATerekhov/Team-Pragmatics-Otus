namespace Services.Contracts.TravelPoint
{
    /// <summary>
    /// Модель точки остановки.
    /// </summary>
    public class TravelPointModel
    {
        /// <summary>
        /// Идентификатор.
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
