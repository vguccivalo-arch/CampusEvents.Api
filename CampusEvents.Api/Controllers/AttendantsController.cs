using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampusEvents.Api.Models;
using CampusEvents.Api.Data;
using CampusEvents.Api.DTOs;

namespace CampusEvents.Api.Controllers;

[ApiController]
[Route("api/attendants")]
public class AttendantsController : ControllerBase
{

    // constructor injection of the ApplicationDbContext
    private readonly ApplicationDbContext _context;
    public AttendantsController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<Attendant>>> GetAttendants
    (
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query = _context.Attendants.AsNoTracking();
        var totalCount = await query.CountAsync();
        var attendants = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var response = new PaginatedResponse<Attendant>
        {
            Page = page,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = page,
            Items = attendants
        };

        return Ok(response);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Attendant>> GetAttendantById(int id)
    {
        var attendant = await _context.Attendants.FirstOrDefaultAsync(a => a.Id == id);

        if (attendant == null)
        {
            return NotFound(new { message = $"Attendant with ID {id} not found." });
        }
        return Ok(attendant);
    }
    [HttpPost]
    public async Task<ActionResult<Attendant>> CreateAttendant(AttendantCreateDDto attendant)
    {
        var attendantModel = new Attendant
        {
            Name = attendant.Name,
            Email = attendant.Email
        };
        _context.Attendants.Add(attendantModel);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAttendants), new { id = attendantModel.Id }, attendantModel);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAttendant(int id, [FromBody] Attendant updatedAttendant)
    {
        var existingAttendant = await _context.Attendants.FirstOrDefaultAsync(a => a.Id == id);

        if (existingAttendant == null)
        {
            return NotFound(new { message = $"Attendant with ID {id} not found." });
        }

        existingAttendant.Name = updatedAttendant.Name;
        existingAttendant.Email = updatedAttendant.Email;

        await _context.SaveChangesAsync();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAttendant(int id)
    {
        var existingAttendant = await _context.Attendants.FirstOrDefaultAsync(a => a.Id == id);

        if (existingAttendant == null)
        {
            return NotFound(new { message = $"Attendant with ID {id} not found." });
        }

        _context.Attendants.Remove(existingAttendant);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
