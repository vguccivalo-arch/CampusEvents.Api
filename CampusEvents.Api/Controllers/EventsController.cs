using Microsoft.AspNetCore.Mvc;
using CampusEvents.Api.Models;
using CampusEvents.Api.Data;

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
    public ActionResult<IEnumerable<CampusEvent>> GetEvents()
    {
        var events = _context.Events.ToList();
        return Ok(events);
    }
    [HttpGet("{id}")]
    public ActionResult<CampusEvent> GetEventById(int id)
    {
        var campusevent = _context.Events.FirstOrDefault(e => e.Id == id);

        if (campusevent == null)
        {
            return NotFound(new { message = $"Event with ID {id} not found." });
        }
        return Ok(campusevent);
    }
    [HttpPost]
    public ActionResult<CampusEvent> CreateEvent(EventCreateDto campusEvent)
    {
        var eventModel = new CampusEvent
        {
            Name = campusEvent.Name,
            Description = campusEvent.Description,
            Date = campusEvent.Date,
            Location = campusEvent.Location
        };
        _context.Events.Add(eventModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetEvents), new { id = eventModel.Id }, eventModel);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateEvent(int id, [FromBody] CampusEvent updatedEvent)
    {

        var existingEvent = _context.Events.FirstOrDefault(e => e.Id == id);


        if (existingEvent == null)
        {
            return NotFound(new { message = $"Event with ID {id} not found." });
        }

        existingEvent.Name = updatedEvent.Name;
        existingEvent.Date = updatedEvent.Date;
        existingEvent.Location = updatedEvent.Location;

        return NoContent();
    }
    [HttpDelete("{id}")]
    public ActionResult DeleteEvent(int id)
    {
        var existingEvent = _context.Events.FirstOrDefault(e => e.Id == id);

        if (existingEvent == null)
        {
            return NotFound(new { message = $"Event with ID {id} not found." });
        }

        _context.Events.Remove(existingEvent);
        _context.SaveChanges();

        return NoContent();
    }

}
