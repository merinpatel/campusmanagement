using GeoLocationproject.Models;

namespace GeoLocationproject.Repositories
{
    public interface IAlertRepository
    {
        Task<List<Alert>> GetAllAsync();
        Task<Alert> AddAsync(Alert alert);
    }

}
