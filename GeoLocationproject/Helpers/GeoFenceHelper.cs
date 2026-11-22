using GeoLocationproject.Data;
using Microsoft.EntityFrameworkCore;

public class GeoFenceHelper
{
    private readonly AppDbContext _context;
    public GeoFenceHelper(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<GeoFenceEvent>> CheckGeoFencesAsync(
        int busId, double lat, double lng)
    {
        var events = new List<GeoFenceEvent>();
        var fences = await _context.GeoFences.ToListAsync();

        foreach (var fence in fences)
        {
            var distance = DistanceHelper.DistanceInMeters(
                lat, lng, fence.CenterLatitude, fence.CenterLongitude);

            if (distance <= fence.RadiusMeters)
            {
                events.Add(new GeoFenceEvent
                {
                    Type = "EnteredGeoFence",
                    Message = $"Bus {busId} entered {fence.Name}"
                });
            }
        }
        return events;
    }
}

public class GeoFenceEvent
{
    public string Type { get; set; }
    public string Message { get; set; }
}
