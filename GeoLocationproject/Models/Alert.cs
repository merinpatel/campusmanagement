namespace GeoLocationproject.Models
{
    public class Alert
    {
        public long AlertId { get; set; }
        public int? BusId { get; set; }
        public int? StudentId { get; set; }
        public string? AlertType { get; set; }
        public string Message { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
    }
}
