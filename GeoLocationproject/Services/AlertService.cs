using GeoLocationproject.Models;
using GeoLocationproject.Repositories;

public class AlertService
{
    private readonly IAlertRepository _repo;
    public AlertService(IAlertRepository repo)
    {
        _repo = repo;
    }

    public Task<List<Alert>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Alert> AddAsync(Alert alert) => _repo.AddAsync(alert);
}
