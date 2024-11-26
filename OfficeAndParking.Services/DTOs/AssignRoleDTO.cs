using System.ComponentModel.DataAnnotations;

namespace OfficeAndParkingAPI.Services.DTOs
{
    public class AssignRoleDTO
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
