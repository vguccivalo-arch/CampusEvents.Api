using System.ComponentModel.DataAnnotations;
namespace CampusEvents.Api.Models;

public class EventCategoryResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class EventCategoryCreateDDto
{
    [Required]
    public string Name { get; set; }
}
public class EventCategoryUpdateDto
{
    [Required]
    public string Name { get; set; }
}
public class EventCategoryDeleteDto
{
    [Required]
    public int Id { get; set; }
}