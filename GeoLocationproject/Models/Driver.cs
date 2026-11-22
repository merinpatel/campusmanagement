namespace GeoLocationproject.Models
{
    public class Driver
    {
        public int DriverId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string LicenseNumber { get; set; }
        public int? AssignedBusId { get; set; }
        public bool IsActive { get; set; }
        public Bus? AssignedBus { get; set; }
    }
}
