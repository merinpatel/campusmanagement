using GeoLocationproject.Models;
using GeoLocationproject.Repositories;

namespace GeoLocationproject.Services
{
    public class BusService
    {
        private readonly IBusRepository _repo;
        public BusService(IBusRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Bus>> GetAllAsync() => _repo.GetAllAsync();
        public Task<List<BusDetailsDto>> getBusDetailsAsync() => _repo.getBusDetailsAsync();
        public Task<Bus?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Bus> AddAsync(Bus bus) => _repo.AddAsync(bus);
        public Task UpdateAsync(Bus bus) => _repo.UpdateAsync(bus);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }

}
