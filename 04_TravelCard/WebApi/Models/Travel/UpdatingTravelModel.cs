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
    }
}
