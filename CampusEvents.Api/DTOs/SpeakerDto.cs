using System.ComponentModel.DataAnnotations;
namespace CampusEvents.Api.Models;

public class speakerResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public string Email { get; set; }
}
public class speakerCreateDDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Bio { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
public class speakerUpdateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Bio { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
public class speakerDeleteDto
{
    [Required]
    public int Id { get; set; }
}