using GeoLocationproject.Models;
using Microsoft.AspNetCore.SignalR;

public class TrackingHub : Hub
{
    public async Task SendBusLocation(LocationLog location)
    {
        // Broadcast live bus location to all connected clients
        Console.WriteLine($"📡 Location received: {location.Latitude}, {location.Longitude}");

        await Clients.All.SendAsync("ReceiveBusLocation", location);
    }

    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("Connected",
            "Live tracking connected successfully");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}
