using Microsoft.EntityFrameworkCore;
using OfficeParkingAndBooking.Data.Models;

namespace OfficeParkingAndBooking.Data
{
    internal class OfficeParkingDbContext:DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<OfficePresence> OfficePresences { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; } 
        public DbSet<Room> Rooms { get; set; } 
        public DbSet<Team> Teams { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\sqlexpress;Database=OfficeAndParkingDB;User Id=sa;Password=test;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().HasData(
                new Team { Id = 1, ShortName = "DotNet", FullName = ".NET Development" },
                new Team { Id = 5, ShortName = "Java", FullName = "Java Development" },
                new Team { Id = 6, ShortName = "BA", FullName = "Business Analyst" },
                new Team { Id = 2, ShortName = "HR", FullName = "Human Resources" },
                new Team { Id = 3, ShortName = "SysAdmin", FullName = "System Administration" },
                new Team { Id = 4, ShortName = "DevOps", FullName = "Development Operations" },
                new Team { Id = 7, ShortName = "AM", FullName = "Account Management" },
                new Team { Id = 8, ShortName = "FO", FullName = "Front Office" }
            );
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Number = 403, RoomCapacity = 8},
                new Room { Id = 2, Number = 404, RoomCapacity = 8}
            );
            modelBuilder.Entity<ParkingSpot>().HasData(
                new ParkingSpot { Id = 1, SpotNumber = 1 },
                new ParkingSpot { Id = 2, SpotNumber = 2 },
                new ParkingSpot { Id = 3, SpotNumber = 3 },
                new ParkingSpot { Id = 4, SpotNumber = 4 }
            );
            // Test data
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Firstname = "Lachezar", Lastname = "Atanasov", TeamId = 1 }
            );
        }
    }
}
