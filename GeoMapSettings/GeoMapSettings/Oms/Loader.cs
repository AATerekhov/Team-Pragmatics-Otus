using GeoMapSettings.Model;
using OsmSharp;
using OsmSharp.Streams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static GeoMapSettings.Model.GeoExtentions;

namespace GeoMapSettings.Oms
{
    public class Loader
    {
        public Loader(string path)
        {
            Path = path;
        }
        public string Path { get; set; }

        public List<Place> Load(PlaceType placeType)
        {
            var result = new List<Place>();
            if (placeType == null) return result;

            using (var fileStream = File.OpenRead(Path))
            {

                var source = new PBFOsmStreamSource(fileStream);
                var expressions = placeType.GetSearchExpressions();
                var geoTypes = placeType.GetSearchGoeTypes();
                foreach (var type in geoTypes)
                {
                    foreach (var expression in expressions)
                    {
                        IEnumerable<OsmGeo> filtered = source.GetFiltered(expression, type);

                        foreach (var item in filtered)
                        {
                            result.Add(item.ConvertOsmGeo(placeType, source));
                        }
                        var complete = filtered.ToComplete();
                    }
                }                
            }
            return result;
        }       
    }
}
