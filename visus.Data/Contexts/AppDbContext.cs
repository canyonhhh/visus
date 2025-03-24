using Microsoft.EntityFrameworkCore;
using visus.Models.Entities;

namespace visus.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<YouthParticipant> YouthParticipants { get; set; }
    }
}