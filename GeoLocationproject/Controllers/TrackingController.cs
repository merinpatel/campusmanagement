using GeoLocationproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

[ApiController]
[Route("api/[controller]")]
public class TrackingController : ControllerBase
{
    private readonly TrackingService _service;
    private readonly IHubContext<TrackingHub> _hub;

    public TrackingController(
        TrackingService service,
        IHubContext<TrackingHub> hub)
    {
        _service = service;
        _hub = hub;
    }

    [HttpPost("location")]
    public async Task<IActionResult> PostLocation(LocationLog dto)
    {
        var log = new LocationLog
        {
            BusId = dto.BusId,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            Speed = dto.Speed,
            RecordedAt = DateTime.UtcNow
        };

        await _service.SaveLocationAsync(log);

        await _hub.Clients.All.SendAsync("ReceiveBusLocation", new
        {
            dto.BusId,
            dto.Latitude,
            dto.Longitude,
            dto.Speed
        });

        return Ok();
    }
}
