using System.ComponentModel.DataAnnotations;

namespace OfficeParkingAndBooking.Data.Models
{
    public class OfficePresence
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int EmployeeId { get; set; }
        public virtual required Employee Employee { get; set; }

        public int RoomId { get; set; }
        public required Room OfficeRoom { get; set; }

        public int? ParkingSpotReservationId { get; set; }
        public virtual ParkingSpotReservation? ParkingSpotReservation { get; set; }

        [MaxLength(100)] public string? Notes { get; set; }

    }
}
