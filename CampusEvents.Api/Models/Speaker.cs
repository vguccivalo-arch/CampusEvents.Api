namespace CampusEvents.Api.Models;

public class CampusSpeaker
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; } = string.Empty;
    public string Sex { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Title { get; set; }
    public string Organization { get; set; }

}