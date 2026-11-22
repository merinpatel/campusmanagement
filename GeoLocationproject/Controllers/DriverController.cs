using GeoLocationproject.Models;
using GeoLocationproject.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DriverController : ControllerBase
{
    private readonly DriverService _service;

    public DriverController(DriverService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var driver = await _service.GetByIdAsync(id);
        return driver == null ? NotFound() : Ok(driver);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Driver driver)
        => Ok(await _service.AddAsync(driver));

    [HttpPut]
    public async Task<IActionResult> Update(Driver driver)
    {
        await _service.UpdateAsync(driver);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}
