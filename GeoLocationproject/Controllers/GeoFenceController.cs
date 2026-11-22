using GeoLocationproject.Data;
using GeoLocationproject.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class GeoFenceController : ControllerBase
{
    private readonly AppDbContext _context;

    public GeoFenceController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
        => Ok(_context.GeoFences.ToList());

    [HttpPost]
    public async Task<IActionResult> Create(GeoFence fence)
    {
        _context.GeoFences.Add(fence);
        await _context.SaveChangesAsync();
        return Ok(fence);
    }
}
