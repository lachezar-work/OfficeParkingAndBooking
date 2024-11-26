using System.ComponentModel.DataAnnotations;
using OfficeAndParkingAPI.Common;

namespace OfficeAndParking.Services.DTOs.EmployeeDTOs
{
    public class UpdateEmployeeDTO
    {
        [StringLength(GlobalConstants.MaxEmployeeFirstNameLength, MinimumLength = GlobalConstants.MinEmployeeFirstNameLength)]
        public string? FirstName { get; init; }
        [StringLength(GlobalConstants.MaxEmployeeLastNameLength, MinimumLength = GlobalConstants.MinEmployeeLastNameLength)]
        public string? LastName { get; init; }
        public int TeamId { get; init; }
        public string? Username { get; set; }

        [MinLength(6)]
        public string? Password { get; set; }
    }
}