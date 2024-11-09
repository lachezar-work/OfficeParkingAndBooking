using System.ComponentModel.DataAnnotations;

namespace OfficeParkingAndBooking.Data.Models
{
    public class Car
    {
        public int Id { get; set; }
        [MaxLength(30)] public required string Brand { get; set; }
        [MaxLength(20)] public required string RegistrationPlate { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public virtual ParkingSpotReservation? ParkingSpotReservation { get; set; }
    }
}
