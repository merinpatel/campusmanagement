using GeoLocationproject.Models;
using GeoLocationproject.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BusController : ControllerBase
{
    private readonly BusService _service;

    public BusController(BusService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("busdetails_api")]
    public async Task<IActionResult> getBusDetails()
        => Ok(await _service.getBusDetailsAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var bus = await _service.GetByIdAsync(id);
        return bus == null ? NotFound() : Ok(bus);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Bus bus)
        => Ok(await _service.AddAsync(bus));

    [HttpPut]
    public async Task<IActionResult> Update(Bus bus)
    {
        await _service.UpdateAsync(bus);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}
