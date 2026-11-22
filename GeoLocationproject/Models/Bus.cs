namespace GeoLocationproject.Models
{
    public class Bus
    {
        public int BusId { get; set; }
        public string BusNumber { get; set; }
        public int? Capacity { get; set; }
        public string RouteName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<LocationLog>? LocationLogs { get; set; }
    }
}
