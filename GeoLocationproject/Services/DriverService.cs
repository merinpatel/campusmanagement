using GeoLocationproject.Models;
using GeoLocationproject.Repositories;

namespace GeoLocationproject.Services
{
    public class DriverService
    {
        private readonly IDriverRepository _repo;
        public DriverService(IDriverRepository repo)
        {
            _repo = repo;
        }

        public Task<List<Driver>> GetAllAsync() => _repo.GetAllAsync();
        public Task<Driver?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<Driver> AddAsync(Driver driver) => _repo.AddAsync(driver);
        public Task UpdateAsync(Driver driver) => _repo.UpdateAsync(driver);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }

}
