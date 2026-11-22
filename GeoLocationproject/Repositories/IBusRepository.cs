using GeoLocationproject.Models;

namespace GeoLocationproject.Repositories
{
    public interface IBusRepository
    {
        Task<List<Bus>> GetAllAsync();
        Task<List<BusDetailsDto>> getBusDetailsAsync();
        Task<Bus?> GetByIdAsync(int id);
        Task<Bus> AddAsync(Bus bus);
        Task UpdateAsync(Bus bus);
        Task DeleteAsync(int id);
    }

}
