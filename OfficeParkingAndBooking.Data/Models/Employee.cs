using System.ComponentModel.DataAnnotations;

namespace OfficeAndParking.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        
        public int TeamId { get; set; } 
        public Team? Team { get; set; }

        public virtual HashSet<OfficePresence>? OfficePresences { get; set; } = new();
        public virtual HashSet<Car>? Cars { get; set; } = new();
    }
}
