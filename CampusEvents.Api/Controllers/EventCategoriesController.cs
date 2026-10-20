using Microsoft.AspNetCore.Mvc;
using CampusEvents.Api.Models;
using CampusEvents.Api.Data;
namespace CampusEvents.Api.Controllers;

[ApiController]
[Route("api/eventcategories")]
public class EventCategoriesController : ControllerBase
{

    // constructor injection of the ApplicationDbContext
    private readonly ApplicationDbContext _context;
    public EventCategoriesController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public ActionResult<IEnumerable<EventCategory>> GetEventCategories()
    {
        var _eventcategories = _context.EventCategories.ToList();
        return Ok(_eventcategories);
    }
    [HttpGet("{id}")]
    public ActionResult<EventCategory> GetEventCategoryById(int id)
    {
        var eventCategory = _context.EventCategories.FirstOrDefault(ec => ec.Id == id);

        if (eventCategory == null)
        {
            return NotFound(new { message = $"Event category with ID {id} not found." });
        }
        return Ok(eventCategory);
    }
    [HttpPost]
    public ActionResult<EventCategory> CreateEventCategory(EventCategoryCreateDDto eventCategory)
    {
        var eventCategoryModel = new EventCategory
        {
            Name = eventCategory.Name
        };
        _context.EventCategories.Add(eventCategoryModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetEventCategories), new { id = eventCategoryModel.Id }, eventCategoryModel);
    }
    [HttpPut("{id}")]
    public ActionResult UpdateEventCategory(int id, [FromBody] EventCategory updatedEventCategory)
    {
        var existingEventCategory = _context.EventCategories.FirstOrDefault(ec => ec.Id == id);

        if (existingEventCategory == null)
        {
            return NotFound(new { message = $"Event category with ID {id} not found." });
        }

        existingEventCategory.Name = updatedEventCategory.Name;

        _context.SaveChanges();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public ActionResult DeleteEventCategory(int id)
    {
        var existingEventCategory = _context.EventCategories.FirstOrDefault(ec => ec.Id == id);

        if (existingEventCategory == null)
        {
            return NotFound(new { message = $"Event category with ID {id} not found." });
        }

        _context.EventCategories.Remove(existingEventCategory);
        _context.SaveChanges();

        return NoContent();
    }
}