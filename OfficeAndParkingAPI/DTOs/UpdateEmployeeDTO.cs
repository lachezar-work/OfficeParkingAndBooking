using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using OfficeAndParking.Data.Models;
using OfficeAndParkingAPI.Common;
using System.ComponentModel.DataAnnotations;

namespace OfficeAndParkingAPI.DTOs
{
    public class UpdateEmployeeDTO
    {
        [StringLength(GlobalConstants.MaxEmployeeFirstNameLength, MinimumLength = GlobalConstants.MinEmployeeFirstNameLength)]
        public string? FirstName { get; init; }
        [StringLength(GlobalConstants.MaxEmployeeLastNameLength, MinimumLength = GlobalConstants.MinEmployeeLastNameLength)]
        public string? LastName { get; init; }
        public int TeamId { get; init; }

        [EmailAddress]
        public string? Email { get; set; }

        [MinLength(6)]
        public string? Password { get; set; }
    }
}