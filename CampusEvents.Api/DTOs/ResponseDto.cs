using System.ComponentModel.DataAnnotations;

namespace CampusEvents.Api.DTOs;

public class PaginatedResponse<T>
{
    public int Page { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
    public List<T> Items { get; set; } = new List<T>();
}