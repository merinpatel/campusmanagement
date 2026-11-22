namespace GeoLocationproject.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string RollNumber { get; set; }
        public string? ParentName { get; set; }
        public string ParentPhone { get; set; }
        public int? AssignedBusId { get; set; }
        public bool IsActive { get; set; }
        public string PickupPoint { get; set; }
    }
}
