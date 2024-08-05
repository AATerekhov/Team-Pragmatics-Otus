using OsmSharp;
using OsmSharp.Streams;
using OsmSharp.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GeoMapSettings.Model
{
    public static class GeoExtentions
    {
        public static List<SearchExpression> GetSearchExpressions(this PlaceType placeType) 
        {
            return GetTagsForPlaceType()[placeType.Name];  
        }
        /// <summary>
        /// Проверяются максимум три условия одновременно.
        /// </summary>
        /// <returns></returns>
        static Dictionary<string, List<SearchExpression>> GetTagsForPlaceType() 
        {
            return new Dictionary<string, List<SearchExpression>>()
            {
                {"Cafe",new List<SearchExpression>(){ 
                    new SearchExpression("amenity=cafe") 
                } },
                {"Museum",new List<SearchExpression>(){
                    new SearchExpression("tourism=museum")
                } },
                {"Landscape", new List<SearchExpression>(){
                    new SearchExpression( "tourism=viewpoint" ),
                    new SearchExpression( "tourism=attraction" )
                } },
                {"Monument", new List<SearchExpression>(){
                     new SearchExpression( "historic=monument" ) ,
                     new SearchExpression( "historic=memorial" )
                } },
                {"Animals",new List<SearchExpression>(){
                    new SearchExpression("tourism=zoo")
                } }
            };
        }
        //Получить типы объектов на карте для указанного типа места.
        public static List<OsmSharp.OsmGeoType> GetSearchGoeTypes(this PlaceType placeType)
        {
            return GetGeoTypesForPlaceType()[placeType.Name].Select(n=> (OsmSharp.OsmGeoType)Enum.GetValues(typeof(OsmSharp.OsmGeoType)).GetValue(n)).ToList();
        }
        //Задать обрабатываемые типы Гео объектов.
        static Dictionary<string, int[]> GetGeoTypesForPlaceType()
        {
            return new Dictionary<string, int[]>()
            {
                {"Cafe", new int[]{ 0}},
                {"Museum",new int[]{ 0 }},
                {"Landscape", new int[]{0 }},
                {"Monument", new int[]{ 0}},
                {"Animals",new int[]{0 }}
            };
        }

        public static Place ConvertOsmGeo(this OsmSharp.OsmGeo osmGeo, PlaceType placeType, PBFOsmStreamSource source)
        {
            var result = new Place() { PlaceTypeID = placeType.Id };
            if (osmGeo is Node geoNode)
            {
                result.Longitude = double.Round((double)geoNode.Longitude,6);
                result.Latitude = double.Round((double)geoNode.Latitude, 6);
                result.Name = geoNode.GetNameTag();
                result.Description =geoNode.GetAllTags();
            }
            else if (osmGeo is Way geoWay)
            {
                var nodes = geoWay.Nodes.Select(n => source.GetOsmGeoForId(OsmGeoType.Node, n)).ToList();
                result.Name = geoWay.GetNameTag();
                result.Description ="W " + geoWay.GetAllTags();
                nodes.ToComplete();
            }
            else if (osmGeo is Relation geoRelation) 
            {

                result.Name = geoRelation.GetNameTag();
                result.Description ="R " + geoRelation.GetAllTags();
            }
            return result;
        }

        static string GetNameTag(this OsmGeo node) 
        {
            var result = node.Tags.FirstOrDefault(t=>t.Key.Contains("name"));
            return result.Value;
        }
        static string GetScription(this OsmGeo node) 
        {
            var result = string.Empty;
            foreach (var item in node.Tags.Where(t => t.Key.Contains("scription")))
            {
                result += item.Value;
                result += "\r\n";
            };

            if (result == string.Empty)
            {
                if (node.Tags.ContainsKey("memorial"))
                {
                    result += node.Tags["memorial"];
                    result += "\r\n";
                }
            }
            return $"[{node.Tags.Count}] " + result.TrimEnd();
        }
        static string GetAllTags(this OsmGeo node)
        {
            var result = string.Empty;
            foreach (var item in node.Tags)
            {
                result += item.TagLog();
                result += " | ";
            }
            return result.TrimEnd();
        }

        static string TagLog(this Tag tag)
        {
            return $"{tag.Key}:{tag.Value}";
        }

        public class SearchExpression 
        {
            public int Count { get => Expression.Count; }        
            public List<(string key, string value)> Expression { get; set; }
            public SearchExpression(params string[] expression) 
            {
                Expression = new List<(string key, string value)>();
                foreach (var item in expression)
                {
                    var vs = item.Split('=',2);
                    Expression.Add((vs[0], vs[1]));
                }
            }
        }
    }
}
