using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampusEvents.Api.Models;
using CampusEvents.Api.Data;
using CampusEvents.Api.DTOs;

namespace CampusEvents.Api.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    // constructor injection of the ApplicationDbContext
    private readonly ApplicationDbContext _context;
    public EventsController(ApplicationDbContext context)
    {
        _context = context;
    }



    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<CampusEvent>>> GetEvents
    (
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null

    )
    {
        var query = _context.Events.AsNoTracking();
        if (startDate.HasValue)
        {
            query = query.Where(e => e.Date >= startDate.Value);
        }
        if (endDate.HasValue)
        {
            query = query.Where(e => e.Date <= endDate.Value);
        }
        var totalCount = await _context.Events.CountAsync();
        var events = await _context.Events
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var response = new PaginatedResponse<CampusEvent>
        {
            Page = page,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = page,
            Items = events
        };

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CampusEvent>> GetEventById(int id)
    {
        var campusevent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

        if (campusevent == null)
        {
            return NotFound(new { message = $"Event with ID {id} not found." });
        }
        return Ok(campusevent);
    }
    [HttpPost]
    public async Task<ActionResult<CampusEvent>> CreateEvent(EventCreateDto campusEvent)
    {
        var eventModel = new CampusEvent
        {
            Name = campusEvent.Name,
            Description = campusEvent.Description,
            Date = campusEvent.Date,
            Location = campusEvent.Location
        };
        _context.Events.Add(eventModel);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEvents), new { id = eventModel.Id }, eventModel);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEvent(int id, [FromBody] CampusEvent updatedEvent)
    {

        var existingEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);


        if (existingEvent == null)
        {
            return NotFound(new { message = $"Event with ID {id} not found." });
        }

        existingEvent.Name = updatedEvent.Name;
        existingEvent.Date = updatedEvent.Date;
        existingEvent.Location = updatedEvent.Location;

        await _context.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEvent(int id)
    {
        var existingEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);

        if (existingEvent == null)
        {
            return NotFound(new { message = $"Event with ID {id} not found." });
        }

        _context.Events.Remove(existingEvent);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
