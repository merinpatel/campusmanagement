namespace GeoLocationproject.Repositories
{
    using GeoLocationproject.Data;
    using GeoLocationproject.Models;
    using Microsoft.EntityFrameworkCore;

    public class BusRepository : IBusRepository
    {
        private readonly AppDbContext _context;
        public BusRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Bus>> GetAllAsync()
            => await _context.Buses.ToListAsync();

        public async Task<List<BusDetailsDto>> getBusDetailsAsync()
        {
            var buses = await _context.Buses
                .AsNoTracking()
                .ToListAsync();

            var busIds = buses.Select(bus => bus.BusId).ToList();

            var drivers = await _context.Drivers
                .AsNoTracking()
                .Where(driver => driver.AssignedBusId.HasValue && busIds.Contains(driver.AssignedBusId.Value))
                .Select(driver => new
                {
                    driver.AssignedBusId,
                    Details = new DriverDetailsDto
                    {
                        DriverId = driver.DriverId,
                        Name = driver.Name,
                        Phone = driver.Phone,
                        LicenseNumber = driver.LicenseNumber,
                        IsActive = driver.IsActive
                    }
                })
                .ToListAsync();

            var students = await _context.Students
                .AsNoTracking()
                .Where(student => student.AssignedBusId.HasValue && busIds.Contains(student.AssignedBusId.Value))
                .Select(student => new
                {
                    AssignedBusId = student.AssignedBusId!.Value,
                    Details = new StudentDetailsDto
                    {
                        StudentId = student.StudentId,
                        Name = student.Name,
                        RollNumber = student.RollNumber,
                        ParentName = student.ParentName,
                        ParentPhone = student.ParentPhone,
                        IsActive = student.IsActive,
                        PickupPoint = student.PickupPoint
                    }
                })
                .ToListAsync();

            var locations = await _context.LocationLogs
                .AsNoTracking()
                .Where(location => busIds.Contains(location.BusId))
                .OrderByDescending(location => location.RecordedAt)
                .Select(location => new
                {
                    location.BusId,
                    Details = new LocationDetailsDto
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        Speed = location.Speed,
                        RecordedAt = location.RecordedAt
                    }
                })
                .ToListAsync();

            var driverByBus = drivers
                .Where(driver => driver.AssignedBusId.HasValue)
                .GroupBy(driver => driver.AssignedBusId!.Value)
                .ToDictionary(group => group.Key, group => group.First().Details);

            var studentsByBus = students
                .GroupBy(student => student.AssignedBusId)
                .ToDictionary(group => group.Key, group => group.Select(student => student.Details).ToList());

            var latestLocationByBus = locations
                .GroupBy(location => location.BusId)
                .ToDictionary(group => group.Key, group => group.First().Details);

            return buses.Select(bus => new BusDetailsDto
            {
                BusId = bus.BusId,
                BusNumber = bus.BusNumber,
                Capacity = bus.Capacity,
                RouteName = bus.RouteName,
                IsActive = bus.IsActive,
                CreatedAt = bus.CreatedAt,
                Driver = driverByBus.GetValueOrDefault(bus.BusId),
                Students = studentsByBus.GetValueOrDefault(bus.BusId) ?? new List<StudentDetailsDto>(),
                LatestLocation = latestLocationByBus.GetValueOrDefault(bus.BusId)
            }).ToList();
        }

        public async Task<Bus?> GetByIdAsync(int id)
            => await _context.Buses.FindAsync(id);

        public async Task<Bus> AddAsync(Bus bus)
        {
            _context.Buses.Add(bus);
            await _context.SaveChangesAsync();
            return bus;
        }

        public async Task UpdateAsync(Bus bus)
        {
            _context.Buses.Update(bus);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bus = await _context.Buses.FindAsync(id);
            if (bus != null)
            {
                _context.Buses.Remove(bus);
                await _context.SaveChangesAsync();
            }
        }
    }

}
