namespace GeoMap.Model.Road
{
    public class RoadModel
    {
        public ICollection<RoadPointModel>? RoadPoints { get; set; }
        public double Offset { get; set; }
    }
}
