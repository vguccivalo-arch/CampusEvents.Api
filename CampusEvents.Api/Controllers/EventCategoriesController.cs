using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampusEvents.Api.Models;
using CampusEvents.Api.Data;
using CampusEvents.Api.DTOs;
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
    public async Task<ActionResult<PaginatedResponse<EventCategory>>> GetEventCategories
    (
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query = _context.EventCategories.AsNoTracking();
        var totalCount = await query.CountAsync();
        var eventCategories = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var response = new PaginatedResponse<EventCategory>
        {
            Page = page,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = page,
            Items = eventCategories
        };

        return Ok(response);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<EventCategory>> GetEventCategoryById(int id)
    {
        var eventCategory = await _context.EventCategories.FirstOrDefaultAsync(ec => ec.Id == id);

        if (eventCategory == null)
        {
            return NotFound(new { message = $"Event category with ID {id} not found." });
        }
        return Ok(eventCategory);
    }
    [HttpPost]
    public async Task<ActionResult<EventCategory>> CreateEventCategory(EventCategoryCreateDDto eventCategory)
    {
        var eventCategoryModel = new EventCategory
        {
            Name = eventCategory.Name
        };
        _context.EventCategories.Add(eventCategoryModel);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetEventCategories), new { id = eventCategoryModel.Id }, eventCategoryModel);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEventCategory(int id, [FromBody] EventCategory updatedEventCategory)
    {
        var existingEventCategory = await _context.EventCategories.FirstOrDefaultAsync(ec => ec.Id == id);

        if (existingEventCategory == null)
        {
            return NotFound(new { message = $"Event category with ID {id} not found." });
        }

        existingEventCategory.Name = updatedEventCategory.Name;

        await _context.SaveChangesAsync();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEventCategory(int id)
    {
        var existingEventCategory = await _context.EventCategories.FirstOrDefaultAsync(ec => ec.Id == id);

        if (existingEventCategory == null)
        {
            return NotFound(new { message = $"Event category with ID {id} not found." });
        }

        _context.EventCategories.Remove(existingEventCategory);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}