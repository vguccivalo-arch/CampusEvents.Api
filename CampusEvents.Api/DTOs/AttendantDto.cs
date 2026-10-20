using System.ComponentModel.DataAnnotations;
namespace CampusEvents.Api.Models;

public class AttendantResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
public class AttendantCreateDDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
public class AttendantUpdateDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
public class AttendantDeleteDto
{
    [Required]
    public int Id { get; set; }
}