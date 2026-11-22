using GeoLocationproject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AssignmentController : ControllerBase
{
    private readonly AppDbContext _context;

    public AssignmentController(AppDbContext context)
    {
        _context = context;
    }

    /* ---------------- ASSIGN DRIVER ---------------- */

    [HttpPost("AssignDriver")]
    public async Task<IActionResult> AssignDriver(DriverAssignmentRequest request)
    {
        var bus = await _context.Buses.FindAsync(request.BusId);
        if (bus == null)
            return NotFound(new { message = "Bus not found" });

        var driver = await _context.Drivers.FindAsync(request.DriverId);
        if (driver == null)
            return NotFound(new { message = "Driver not found" });

        // Prevent multiple drivers per bus
        var existingDriver = await _context.Drivers
            .FirstOrDefaultAsync(d => d.AssignedBusId == request.BusId && d.DriverId != request.DriverId);

        if (existingDriver != null)
            return BadRequest(new { message = "This bus already has a driver assigned" });

        driver.AssignedBusId = request.BusId;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Driver assigned successfully" });
    }

    /* ---------------- ASSIGN STUDENT ---------------- */

    [HttpPost("AssignStudent")]
    public async Task<IActionResult> AssignStudent(StudentAssignmentRequest request)
    {
        var bus = await _context.Buses.FindAsync(request.BusId);
        if (bus == null)
            return NotFound(new { message = "Bus not found" });

        var student = await _context.Students.FindAsync(request.StudentId);
        if (student == null)
            return NotFound(new { message = "Student not found" });

        // Prevent duplicate assignment
        if (student.AssignedBusId == request.BusId)
            return BadRequest(new { message = "Student already assigned to this bus" });

        student.AssignedBusId = request.BusId;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Student assigned successfully" });
    }

    /* ---------------- OPTIONAL: REMOVE DRIVER ---------------- */

    [HttpPost("RemoveDriver")]
    public async Task<IActionResult> RemoveDriver(RemoveDriverRequest request)
    {
        var driver = await _context.Drivers.FindAsync(request.DriverId);
        if (driver == null)
            return NotFound(new { message = "Driver not found" });

        driver.AssignedBusId = null;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Driver removed from bus" });
    }

    /* ---------------- OPTIONAL: REMOVE STUDENT ---------------- */

    [HttpPost("RemoveStudent")]
    public async Task<IActionResult> RemoveStudent(RemoveStudentRequest request)
    {
        var student = await _context.Students.FindAsync(request.StudentId);
        if (student == null)
            return NotFound(new { message = "Student not found" });

        student.AssignedBusId = null;

        await _context.SaveChangesAsync();

        return Ok(new { message = "Student removed from bus" });
    }
}

public record DriverAssignmentRequest(int BusId, int DriverId);
public record StudentAssignmentRequest(int BusId, int StudentId);
public record RemoveDriverRequest(int DriverId);
public record RemoveStudentRequest(int StudentId);
