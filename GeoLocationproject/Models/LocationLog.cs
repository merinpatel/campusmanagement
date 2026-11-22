namespace GeoLocationproject.Models
{
    public class LocationLog
    {
        public long LocationLogId { get; set; }
        public int BusId { get; set; }
        public Bus? Bus { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Speed { get; set; }
        public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
    }
}
