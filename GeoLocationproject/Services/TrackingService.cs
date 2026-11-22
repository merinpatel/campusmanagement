using GeoLocationproject.Models;
using GeoLocationproject.Repositories;

public class TrackingService
{
    private readonly ITrackingRepository _trackingRepo;
    private readonly IAlertRepository _alertRepo;
    private readonly GeoFenceHelper _geoHelper;

    public TrackingService(
        ITrackingRepository trackingRepo,
        IAlertRepository alertRepo,
        GeoFenceHelper geoHelper)
    {
        _trackingRepo = trackingRepo;
        _alertRepo = alertRepo;
        _geoHelper = geoHelper;
    }

    public async Task<LocationLog> SaveLocationAsync(LocationLog log)
    {
        var savedLog = await _trackingRepo.AddLocationAsync(log);

        var events = await _geoHelper.CheckGeoFencesAsync(
            log.BusId, log.Latitude, log.Longitude);

        foreach (var ev in events)
        {       
            var alert = new Alert
            {
                BusId = log.BusId,
                AlertType = ev.Type,
                Message = ev.Message,
                Latitude = log.Latitude,
                Longitude = log.Longitude
            };
            await _alertRepo.AddAsync(alert);
        }

        return savedLog;
    }
}
