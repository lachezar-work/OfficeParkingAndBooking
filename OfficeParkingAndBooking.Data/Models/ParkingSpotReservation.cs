namespace OfficeParkingAndBooking.Data.Models
{
    public class ParkingSpotReservation
    {
        public int Id { get; set; }

        public int ParkingSpotId { get; set; }
        public virtual required ParkingSpot ParkingSpot { get; set; }

        public int CarId { get; set; }
        public virtual required Car CarReservedTheSpot { get; set; }

        public TimeOnly ReservedFrom { get; set; }
        public TimeOnly ReservedUntil { get; set; }

        public virtual required OfficePresence OfficePresence { get; set; }
    }
}
