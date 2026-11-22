using GeoLocationproject.Data;
using GeoLocationproject.Models;
using GeoLocationproject.Repositories;
using Microsoft.EntityFrameworkCore;

public class AlertRepository : IAlertRepository
{
    private readonly AppDbContext _context;
    public AlertRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Alert>> GetAllAsync()
        => await _context.Alerts.OrderByDescending(a => a.CreatedAt).ToListAsync();

    public async Task<Alert> AddAsync(Alert alert)
    {
        _context.Alerts.Add(alert);
        await _context.SaveChangesAsync();
        return alert;
    }
}
