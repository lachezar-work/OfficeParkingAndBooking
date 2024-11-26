using System.ComponentModel.DataAnnotations;

namespace OfficeAndParking.Services.DTOs.EmployeeDTOs
{
    public class LoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
