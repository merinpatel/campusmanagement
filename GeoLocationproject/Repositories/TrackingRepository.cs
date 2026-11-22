using GeoLocationproject.Data;
using GeoLocationproject.Models;
using GeoLocationproject.Repositories;

public class TrackingRepository : ITrackingRepository
{
    private readonly AppDbContext _context;
    public TrackingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LocationLog> AddLocationAsync(LocationLog log)
    {
        _context.LocationLogs.Add(log);
        await _context.SaveChangesAsync();
        return log;
    }
}
