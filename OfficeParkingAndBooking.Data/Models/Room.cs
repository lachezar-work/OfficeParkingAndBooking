namespace OfficeParkingAndBooking.Data.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int RoomCapacity { get; set; }
        public virtual HashSet<OfficePresence>? OfficePresences { get; set; } = new();
    }
}
