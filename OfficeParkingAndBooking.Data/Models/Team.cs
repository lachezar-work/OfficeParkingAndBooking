using System.ComponentModel.DataAnnotations;

namespace OfficeParkingAndBooking.Data.Models
{
    public class Team
    {
        public int Id { get; set; }
        [MaxLength(20)] public required string ShortName { get; set; }
        [MaxLength(50)] public required string FullName { get; set; }

        public virtual HashSet<Employee>? Employees { get; set; } = new();
    }
}
