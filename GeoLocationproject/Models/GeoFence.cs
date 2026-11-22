namespace GeoLocationproject.Models
{
    public class GeoFence
    {
        public int GeoFenceId { get; set; }
        public string Name { get; set; }
        public double CenterLatitude { get; set; }
        public double CenterLongitude { get; set; }
        public int RadiusMeters { get; set; }
    }
}
