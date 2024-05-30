
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
        public string TravelDesc { get; set; }

        /// <summary>
        /// Удалено.
        /// </summary>
        public bool Deleted { get; set; }

    }
}
