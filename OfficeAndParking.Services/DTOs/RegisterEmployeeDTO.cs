using System.ComponentModel.DataAnnotations;
using OfficeAndParkingAPI.Common;

namespace OfficeAndParkingAPI.Services.DTOs
{
    public class RegisterEmployeeDTO(string firstName, string lastName, int teamId, string email, string password)
    {
        [Required]
        [StringLength(GlobalConstants.MaxEmployeeFirstNameLength, MinimumLength = GlobalConstants.MinEmployeeFirstNameLength)]
        public string FirstName { get; init; } = firstName;
        [Required]
        [StringLength(GlobalConstants.MaxEmployeeLastNameLength, MinimumLength = GlobalConstants.MinEmployeeLastNameLength)]
        public string LastName { get; init; } = lastName;
        [Required]
        public int TeamId { get; init; } = teamId;

        [Required] 
        [EmailAddress] 
        public string Email { get; set; } = email;

        [Required]
        [MinLength(6)] 
        public string Password { get; set; } = password;
    }
}
