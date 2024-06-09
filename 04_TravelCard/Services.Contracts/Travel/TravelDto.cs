
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Contracts.Travel
{
    /// <summary>
    /// ДТО путешествия.
    /// </summary>
    public class TravelDto
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

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
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }
    }
}
