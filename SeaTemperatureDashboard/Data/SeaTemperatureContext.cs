using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SeaTemperatureDashboard.Data
{
    public class SeaTemperatureContext : IdentityDbContext<IdentityUser>
    {
        public SeaTemperatureContext(DbContextOptions<SeaTemperatureContext> options) : base(options) { }

        public DbSet<SeaTemperature> SeaTemperatures { get; set; }
        public DbSet<SeaPollution> SeaPollutions { get; set; }
    }

    public class SeaTemperature
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public double Temperature { get; set; }
        public DateTime DateRecorded { get; set; }
    }

    public class SeaPollution
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public double PollutionLevel { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}