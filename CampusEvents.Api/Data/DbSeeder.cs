using Microsoft.EntityFrameworkCore;
using CampusEvents.Api.Models;
using CampusEvents.Api.Data;
using static CampusEvents.Api.Controllers.EventsController;

namespace CampusEvents.Api.Data;

public class DbSeeder
{
    public static void SeedDataBase(ApplicationDbContext context)
    {


        if (!context.Events.Any())
        {



            var events = new List<CampusEvent>
        {
            new CampusEvent { Name = "Tech Conference", Description = "A conference about the latest in technology.", Date = new DateTime(2024, 10, 15), Location = "Auditorium A" },
            new CampusEvent { Name = "Art Exhibition", Description = "An exhibition showcasing local artists.", Date = new DateTime(2024, 11, 5), Location = "Gallery B" },
            new CampusEvent { Name = "Music Festival", Description = "A festival featuring various music bands.", Date = new DateTime(2024, 12, 20), Location = "Outdoor Stage" },
            new CampusEvent { Name = "Science Fair", Description = "A fair to showcase scientific projects and experiments.", Date = new DateTime(2025, 1, 10), Location = "Lab C" },
            new CampusEvent { Name = "Sports Tournament", Description = "A tournament for various sports competitions.", Date = new DateTime(2025, 2, 25), Location = "Stadium D" }
        };


            context.SaveChanges();
        }
    }
}