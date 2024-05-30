namespace Services.Contracts.Travel
{
    /// <summary>
    /// ДТО создания путешествия
    /// </summary>
    public class CreatingTravelDto
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
