using System.ComponentModel.DataAnnotations;

namespace OfficeAndParking.Data.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string FullName { get; set; }

        public virtual HashSet<Employee>? Employees { get; set; } = new();
    }
}
