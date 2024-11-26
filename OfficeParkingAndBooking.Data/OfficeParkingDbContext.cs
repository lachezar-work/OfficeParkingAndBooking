using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OfficeAndParking.Data.Models;
using OfficeAndParkingAPI.Common;

namespace OfficeAndParking.Data
{
    public class OfficeParkingDbContext(DbContextOptions<OfficeParkingDbContext> options) : IdentityDbContext<Employee>(options)
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<OfficePresence> OfficePresences { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; } 
        public DbSet<ParkingSpotReservation> ParkingSpotReservations { get; set; } 
        public DbSet<Room> Rooms { get; set; } 
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OfficePresence>()
                .HasIndex(op => new { op.Date, op.EmployeeId })
                .IsUnique();

            modelBuilder.Entity<Car>()
                .Property(x => x.Brand)
                .IsRequired()
                .HasMaxLength(GlobalConstants.MaxCarBrandLength);
            modelBuilder.Entity<Car>()
                .Property(x => x.RegistrationPlate)
                .IsRequired()
                .HasMaxLength(GlobalConstants.MaxCarRegistrationPlateLength);
            modelBuilder.Entity<Car>()
                .HasIndex(x=>x.RegistrationPlate)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .Property(x => x.Firstname)
                .IsRequired()
                .HasMaxLength(GlobalConstants.MaxEmployeeFirstNameLength);
            modelBuilder.Entity<Employee>()
                .Property(x => x.Lastname)
                .IsRequired()
                .HasMaxLength(GlobalConstants.MaxEmployeeLastNameLength);

            modelBuilder.Entity<Team>()
                .Property(x => x.ShortName)
                .IsRequired()
                .HasMaxLength(GlobalConstants.MaxTeamShortnameLength);
            modelBuilder.Entity<Team>()
                .Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(GlobalConstants.MaxTeamFullnameLength);
            base.OnModelCreating(modelBuilder);

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
            /*
            // Test data
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Firstname = "Lachezar", Lastname = "Atanasov", TeamId = 1
                }
            );
            modelBuilder.Entity<OfficePresence>().HasData(
                new OfficePresence() { Id = 1, Date = DateOnly.Parse("11-11-2024"), RoomId = 1, EmployeeId = 1 }
            );
            modelBuilder.Entity<Car>().HasData(
                new Car() { Id = 1, Brand = "Audi A3", RegistrationPlate = "CB5768MT", EmployeeId = 1 }
                );
            modelBuilder.Entity<ParkingSpotReservation>().HasData(
                new ParkingSpotReservation() { Id = 1, CarId = 1, ParkingSpotId = 1,ReservedFrom = TimeOnly.Parse("09:00:00"),ReservedUntil = TimeOnly.Parse("18:00:00") }
            );
*/
        }
    }
}
