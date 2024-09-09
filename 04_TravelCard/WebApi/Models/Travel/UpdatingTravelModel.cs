namespace Services.Contracts.Travel
{
    /// <summary>
    /// Моедль редактируемого путешесвтия
    /// </summary>
    public class UpdatingTravelModel
    {
        /// <summary>
        /// Описание путешествия
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Координата начала путешествия
        /// </summary>
        public string StartPoint { get; set; }

        /// <summary>
        /// Координата конца путешествия
        /// </summary>
        public string FinishPoint { get; set; }

        /// <summary>
        /// дата и время начала путешествия
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Приватное путешествие
        /// </summary>
        public bool IsPrivate { get; set; }
    }
}
