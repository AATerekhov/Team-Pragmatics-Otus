using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Contracts.Travel
{
    /// <summary>
    /// ДТО редактирования путешествия
    /// </summary>
    public class UpdatingTravelDto
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

    }
}
