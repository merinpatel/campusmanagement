using GeoLocationproject.Models;

namespace GeoLocationproject.Repositories
{
    public interface ITrackingRepository
    {
        Task<LocationLog> AddLocationAsync(LocationLog log);
    }
    
}
