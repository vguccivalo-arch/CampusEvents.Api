
using Microsoft.EntityFrameworkCore;

namespace CampusEvents.Api.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Models.CampusEvent> Events { get; set; }
        public DbSet<Models.Attendant> Attendants { get; set; }
        public DbSet<Models.EventCategory> EventCategories { get; set; }
        public DbSet<Models.CampusSpeaker> Speakers { get; set; }

    }
}