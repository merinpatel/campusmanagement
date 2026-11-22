namespace GeoLocationproject.Models
{
    public sealed class BusDetailsDto
    {
        public int BusId { get; set; }
        public string BusNumber { get; set; } = string.Empty;
        public int? Capacity { get; set; }
        public string RouteName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DriverDetailsDto? Driver { get; set; }
        public List<StudentDetailsDto> Students { get; set; } = new();
        public LocationDetailsDto? LatestLocation { get; set; }
    }

    public sealed class DriverDetailsDto
    {
        public int DriverId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public sealed class StudentDetailsDto
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RollNumber { get; set; } = string.Empty;
        public string? ParentName { get; set; }
        public string ParentPhone { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string PickupPoint { get; set; } = string.Empty;
    }

    public sealed class LocationDetailsDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Speed { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}
