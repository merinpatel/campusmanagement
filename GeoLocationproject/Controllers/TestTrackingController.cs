using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using GeoLocationproject.Models;

[ApiController]
[Route("api/testtracking")]
public class TestTrackingController : ControllerBase
{
    private readonly IHubContext<TrackingHub> _hub;

    public TestTrackingController(IHubContext<TrackingHub> hub)
    {
        _hub = hub;
    }


    [HttpGet("send")]
    public async Task<IActionResult> Send()
    {
        var fakeLocation = new LocationLog
        {
            BusId = 1,
            Latitude = 19.0760,
            Longitude = 72.8777,
            Speed = 50
        };

        await _hub.Clients.All.SendAsync("ReceiveBusLocation", fakeLocation);

        return Ok("Message sent");
    }
}