using Microsoft.AspNetCore.Mvc;
using CampusEvents.Api.Models;
using CampusEvents.Api.Data;

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
    public ActionResult<IEnumerable<Attendant>> GetAttendants()
    {
        var _attendants = _context.Attendants.ToList();
        return Ok(_attendants);

    }
    [HttpGet("{id}")]
    public ActionResult<Attendant> GetAttendantById(int id)
    {
        var attendant = _context.Attendants.FirstOrDefault(a => a.Id == id);

        if (attendant == null)
        {
            return NotFound(new { message = $"Attendant with ID {id} not found." });
        }
        return Ok(attendant);
    }
    [HttpPost]
    public ActionResult<Attendant> CreateAttendant(AttendantCreateDDto attendant)
    {
        var attendantModel = new Attendant
        {
            Name = attendant.Name,
            Email = attendant.Email
        };
        _context.Attendants.Add(attendantModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetAttendants), new { id = attendantModel.Id }, attendantModel);
    }
    [HttpPut("{id}")]
    public ActionResult UpdateAttendant(int id, [FromBody] Attendant updatedAttendant)
    {
        var existingAttendant = _context.Attendants.FirstOrDefault(a => a.Id == id);

        if (existingAttendant == null)
        {
            return NotFound(new { message = $"Attendant with ID {id} not found." });
        }

        existingAttendant.Name = updatedAttendant.Name;
        existingAttendant.Email = updatedAttendant.Email;

        _context.SaveChanges();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public ActionResult DeleteAttendant(int id)
    {
        var existingAttendant = _context.Attendants.FirstOrDefault(a => a.Id == id);

        if (existingAttendant == null)
        {
            return NotFound(new { message = $"Attendant with ID {id} not found." });
        }

        _context.Attendants.Remove(existingAttendant);
        _context.SaveChanges();

        return NoContent();
    }
}
