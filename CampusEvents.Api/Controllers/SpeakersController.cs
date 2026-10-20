using Microsoft.AspNetCore.Mvc;
using CampusEvents.Api.Models;
using CampusEvents.Api.Data;
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
    public ActionResult<IEnumerable<CampusSpeaker>> GetSpeakers()
    {
        var speakers = _context.Speakers.ToList();
        return Ok(speakers);
    }
    [HttpGet("{id}")]
    public ActionResult<CampusSpeaker> GetSpeakerById(int id)
    {
        var campspeaker = _context.Speakers.FirstOrDefault(s => s.Id == id);

        if (campspeaker == null)
        {
            return NotFound(new { message = $"Speaker with ID {id} not found." });
        }
        return Ok(campspeaker);
    }
    [HttpPost]
    public ActionResult<CampusSpeaker> CreateSpeaker(speakerCreateDDto speaker)
    {
        var speakerModel = new CampusSpeaker
        {
            Name = speaker.Name,
            Bio = speaker.Bio,
            Email = speaker.Email
        };
        _context.Speakers.Add(speakerModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetSpeakers), new { id = speakerModel.Id }, speakerModel);
    }
    [HttpPut("{id}")]
    public ActionResult UpdateSpeaker(int id, [FromBody] CampusSpeaker updatedSpeaker)
    {
        var existingSpeaker = _context.Speakers.FirstOrDefault(s => s.Id == id);

        if (existingSpeaker == null)
        {
            return NotFound(new { message = $"Speaker with ID {id} not found." });
        }

        existingSpeaker.Name = updatedSpeaker.Name;
        existingSpeaker.Bio = updatedSpeaker.Bio;
        existingSpeaker.Email = updatedSpeaker.Email;

        _context.SaveChanges();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public ActionResult DeleteSpeaker(int id)
    {
        var existingSpeaker = _context.Speakers.FirstOrDefault(s => s.Id == id);

        if (existingSpeaker == null)
        {
            return NotFound(new { message = $"Speaker with ID {id} not found." });
        }

        _context.Speakers.Remove(existingSpeaker);
        _context.SaveChanges();

        return NoContent();
    }
}
