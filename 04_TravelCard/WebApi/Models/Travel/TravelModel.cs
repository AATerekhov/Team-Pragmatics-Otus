
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Contracts.Travel
{
    /// <summary>
    /// Модель путешествия.
    /// </summary>
    public class TravelModel
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

        /// <summary>
        /// дата и время начала путешествия
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Приватное путешествие
        /// </summary>
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Менеджер путешествия
        /// </summary>
        public Guid UserID { get; set; }
    }
}
