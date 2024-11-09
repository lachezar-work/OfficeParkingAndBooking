using System.ComponentModel.DataAnnotations;

namespace OfficeParkingAndBooking.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [MaxLength(30)] public required string Firstname { get; set; }
        [MaxLength(30)] public required string Lastname { get; set; }
        
        public int TeamId { get; set; } 
        public Team? Team { get; set; }

        public virtual HashSet<OfficePresence>? OfficePresences { get; set; } = new();
        public virtual HashSet<Car>? Cars { get; set; } = new();
    }
}
