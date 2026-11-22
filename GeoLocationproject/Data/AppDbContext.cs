using Microsoft.EntityFrameworkCore;
using GeoLocationproject.Models; 

namespace GeoLocationproject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }   
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<LocationLog> LocationLogs { get; set; }
        public DbSet<GeoFence> GeoFences { get; set; }
        public DbSet<Alert> Alerts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bus>().HasKey(b => b.BusId);
            modelBuilder.Entity<Driver>().HasOne(d => d.AssignedBus).WithMany();
            modelBuilder.Entity<LocationLog>().HasOne(l => l.Bus).WithMany(b => b.LocationLogs).HasForeignKey(l => l.BusId);
        }
    }
}