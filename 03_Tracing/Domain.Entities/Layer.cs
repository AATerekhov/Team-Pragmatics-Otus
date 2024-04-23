using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// Слой мест одного типа.
    /// </summary>
    public class Layer
    {
        //TODO: В БД храниться не будет.
        public virtual PlaceType? LayerType { get; set; }
        public virtual List<Place>? Places { get; set; }
    }
}
