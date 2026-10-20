using System.ComponentModel.DataAnnotations;
namespace CampusEvents.Api.Models;

public class EventResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
}
public class EventCreateDto
{
    // Data annotations for validation
    [Required, StringLength(100, ErrorMessage = "Name must be between 3 and 100 characters long.", MinimumLength = 3)]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public string Location { get; set; }
    // [Required, EmailAddress]
    // public string Email { get; set; }
}
public class EventUpdateDto
{
    [Required, StringLength(100, ErrorMessage = "Name must be between 3 and 100 characters long.", MinimumLength = 3)]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public string Location { get; set; }
}
public class EventDeleteDto
{
    [Required]
    public int Id { get; set; }
}