using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OfficeAndParking.Services.DTOs.EmployeeDTOs;
using OfficeAndParkingAPI.Common;

namespace OfficeAndParking.Services.DTOs.EmployeeDTOs
{
    public class RegisterEmployeeDTO(string firstName, string lastName, int teamId, string username, string password)
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
        public string Username { get; set; } = username;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = password;
    }
}