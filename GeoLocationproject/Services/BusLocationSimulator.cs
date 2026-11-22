using GeoLocationproject.Data;
using GeoLocationproject.Models;
using GeoLocationproject.Repositories;
using GeoLocationproject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

public class BusLocationSimulator : BackgroundService
{
    private const double BaseLatitude = 23.0225;
    private const double BaseLongitude = 72.5714;

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHubContext<TrackingHub> _hub;
    private readonly Dictionary<int, SimulatedBusPosition> _positions = new();
    private readonly Random _random = new();

    public BusLocationSimulator(
        IServiceScopeFactory scopeFactory,
        IHubContext<TrackingHub> hub)
    {
        _scopeFactory = scopeFactory;
        _hub = hub;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(3000, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();

            var busRepo = scope.ServiceProvider.GetRequiredService<IBusRepository>();
            var trackingService = scope.ServiceProvider.GetRequiredService<TrackingService>();
            var alertRepo = scope.ServiceProvider.GetRequiredService<IAlertRepository>();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var busesFromDb = await busRepo.GetAllAsync();

            foreach (var bus in busesFromDb)
            {
                var latestLocation = await dbContext.LocationLogs
                    .Where(log => log.BusId == bus.BusId)
                    .OrderByDescending(log => log.RecordedAt)
                    .FirstOrDefaultAsync(stoppingToken);

                var position = GetNextPosition(bus.BusId, latestLocation);
                var speed = _random.Next(28, 76);

                var log = new LocationLog
                {
                    BusId = bus.BusId,
                    Latitude = Math.Round(position.Latitude, 6),
                    Longitude = Math.Round(position.Longitude, 6),
                    Speed = speed,
                    RecordedAt = DateTime.UtcNow
                };

                await trackingService.SaveLocationAsync(log);

                await _hub.Clients.All.SendAsync("ReceiveBusLocation", new
                {
                    log.BusId,
                    log.Latitude,
                    log.Longitude,
                    log.Speed,
                    log.RecordedAt
                });

                if (speed > 55)
                {
                    var alert = new Alert
                    {
                        BusId = bus.BusId,
                        AlertType = "SpeedViolation",
                        Message = $"Speed violation! Bus speed is {speed} km/h",
                        Latitude = log.Latitude,
                        Longitude = log.Longitude,
                        CreatedAt = DateTime.UtcNow
                    };

                    await alertRepo.AddAsync(alert);

                    await _hub.Clients.All.SendAsync("ReceiveAlert", new
                    {
                        alert.AlertId,
                        alert.BusId,
                        alert.AlertType,
                        alert.Message,
                        alert.Latitude,
                        alert.Longitude,
                        alert.CreatedAt
                    });
                }
            }

            await Task.Delay(5000, stoppingToken);
        }
    }

    private SimulatedBusPosition GetNextPosition(int busId, LocationLog? latestLocation)
    {
        if (!_positions.TryGetValue(busId, out var current))
        {
            current = latestLocation == null
                ? new SimulatedBusPosition(
                    BaseLatitude + ((_random.NextDouble() - 0.5) / 120),
                    BaseLongitude + ((_random.NextDouble() - 0.5) / 120))
                : new SimulatedBusPosition(latestLocation.Latitude, latestLocation.Longitude);
        }

        var next = new SimulatedBusPosition(
            current.Latitude + ((_random.NextDouble() - 0.5) / 1500),
            current.Longitude + ((_random.NextDouble() - 0.5) / 1500));

        _positions[busId] = next;
        return next;
    }

    private readonly record struct SimulatedBusPosition(double Latitude, double Longitude);
}
