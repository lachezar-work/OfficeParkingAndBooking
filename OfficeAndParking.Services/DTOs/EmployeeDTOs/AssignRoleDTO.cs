using System.ComponentModel.DataAnnotations;

namespace OfficeAndParking.Services.DTOs.EmployeeDTOs
{
    public class AssignRoleDTO
    {
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
