using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using OfficeAndParkingAPI.Common;
using System.ComponentModel.DataAnnotations;

namespace OfficeAndParkingAPI.DTOs
{
    public class UpdateEmployeeDTO
    {
        [StringLength(GlobalConstants.MaxEmployeeFirstNameLength, MinimumLength = GlobalConstants.MinEmployeeFirstNameLength)]
        public string? FirstName { get; set; }

        [StringLength(GlobalConstants.MaxEmployeeLastNameLength, MinimumLength = GlobalConstants.MinEmployeeLastNameLength) ]
        public string? LastName { get; set; }
        public int TeamId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}