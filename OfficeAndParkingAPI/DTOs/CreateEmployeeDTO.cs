using System.ComponentModel.DataAnnotations;
using OfficeAndParkingAPI.Common;

namespace OfficeAndParkingAPI.DTOs
{
    public class CreateEmployeeDTO(string firstName, string lastName, int teamId)
    {
        [Required]
        [StringLength(GlobalConstants.MaxEmployeeFirstNameLength, MinimumLength = GlobalConstants.MinEmployeeFirstNameLength)]
        public string FirstName { get; init; } = firstName;
        [Required]
        [StringLength(GlobalConstants.MaxEmployeeLastNameLength, MinimumLength = GlobalConstants.MinEmployeeLastNameLength)]
        public string LastName { get; init; } = lastName;
        [Required]
        public int TeamId { get; init; } = teamId;
    }
}
