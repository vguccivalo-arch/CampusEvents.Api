using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CampusEvents.Api.Models;
using CampusEvents.Api.Data;
using CampusEvents.Api.DTOs;
namespace CampusEvents.Api.Controller;

[ApiController]
[Route("api/speakers")]
public class SpeakersController : ControllerBase
{

    // constructor injection of the ApplicationDbContext
    private readonly ApplicationDbContext _context;
    public SpeakersController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<CampusSpeaker>>> GetSpeakers
    (
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var query = _context.Speakers.AsNoTracking();
        var totalCount = await query.CountAsync();
        var speakers = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var response = new PaginatedResponse<CampusSpeaker>
        {
            Page = page,
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = page,
            Items = speakers
        };

        return Ok(response);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<CampusSpeaker>> GetSpeakerById(int id)
    {
        var campspeaker = await _context.Speakers.FirstOrDefaultAsync(s => s.Id == id);

        if (campspeaker == null)
        {
            return NotFound(new { message = $"Speaker with ID {id} not found." });
        }
        return Ok(campspeaker);
    }
    [HttpPost]
    public async Task<ActionResult<CampusSpeaker>> CreateSpeaker(speakerCreateDDto speaker)
    {
        var speakerModel = new CampusSpeaker
        {
            Name = speaker.Name,
            Bio = speaker.Bio,
            Email = speaker.Email
        };
        _context.Speakers.Add(speakerModel);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSpeakers), new { id = speakerModel.Id }, speakerModel);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSpeaker(int id, [FromBody] CampusSpeaker updatedSpeaker)
    {
        var existingSpeaker = await _context.Speakers.FirstOrDefaultAsync(s => s.Id == id);

        if (existingSpeaker == null)
        {
            return NotFound(new { message = $"Speaker with ID {id} not found." });
        }

        existingSpeaker.Name = updatedSpeaker.Name;
        existingSpeaker.Bio = updatedSpeaker.Bio;
        existingSpeaker.Email = updatedSpeaker.Email;

        await _context.SaveChangesAsync();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSpeaker(int id)
    {
        var existingSpeaker = await _context.Speakers.FirstOrDefaultAsync(s => s.Id == id);

        if (existingSpeaker == null)
        {
            return NotFound(new { message = $"Speaker with ID {id} not found." });
        }

        _context.Speakers.Remove(existingSpeaker);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
