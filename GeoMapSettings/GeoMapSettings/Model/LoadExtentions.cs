using OsmSharp.Streams;
using OsmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeoMapSettings.Model.GeoExtentions;

namespace GeoMapSettings.Model
{
    internal static class LoadExtentions
    {
        internal static IEnumerable<OsmGeo> GetFiltered(this PBFOsmStreamSource source, SearchExpression searchExpression, OsmGeoType typeGeo = OsmSharp.OsmGeoType.Node)
        {
            if (searchExpression.Count == 1)
            {
                return from osmGeo in source
                       where
                        (osmGeo.Type == typeGeo && osmGeo.Tags != null
                        && osmGeo.Tags.Contains(searchExpression.Expression[0].key, searchExpression.Expression[0].value)
                        )
                       select osmGeo;
            }
            else if (searchExpression.Count == 2)
            {
                return from osmGeo in source
                       where
                        (osmGeo.Type == typeGeo && osmGeo.Tags != null
                        && osmGeo.Tags.Contains(searchExpression.Expression[0].key, searchExpression.Expression[0].value)
                        && osmGeo.Tags.Contains(searchExpression.Expression[1].key, searchExpression.Expression[1].value)
                        )
                       select osmGeo;
            }
            else
            {
                return from osmGeo in source
                       where
                        (osmGeo.Type == typeGeo && osmGeo.Tags != null
                        && osmGeo.Tags.Contains(searchExpression.Expression[0].key, searchExpression.Expression[0].value)
                        && osmGeo.Tags.Contains(searchExpression.Expression[1].key, searchExpression.Expression[1].value)
                        && osmGeo.Tags.Contains(searchExpression.Expression[2].key, searchExpression.Expression[2].value)
                        )
                       select osmGeo;
            }
        }
        internal static OsmGeo GetOsmGeoForId(this PBFOsmStreamSource source, OsmGeoType typeGeo,long id) 
        {
            var elements = from osmGeo in source
                   where
                    (osmGeo.Type == typeGeo && osmGeo.Id == id)
                   select osmGeo;
            return elements.FirstOrDefault();
        }
    }
}
