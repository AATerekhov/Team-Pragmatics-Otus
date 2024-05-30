using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Contracts.Travel
{
    /// <summary>
    /// Модель создаваемого путешествия
    /// </summary>
    public class CreatingTravelModel
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
