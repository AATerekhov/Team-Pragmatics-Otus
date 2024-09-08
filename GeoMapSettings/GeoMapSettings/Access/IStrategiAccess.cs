using GeoMapSettings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoMapSettings.Access
{
    public interface IStrategiAccess
    {
        Task MakeTrasaction(Place place);
    }
}
