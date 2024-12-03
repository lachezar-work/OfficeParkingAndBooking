using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeAndParkingAPI.Common;

namespace OfficeAndParking.Services.DTOs.CarDTOs
{
    public class AddNewCarDTO
    {
        public string Brand { get; set; }

        [Range(GlobalConstants.MinCarRegistrationPlateLength, GlobalConstants.MaxCarRegistrationPlateLength, ErrorMessage = "Registration plate length must be between {1} and {2} characters.")]
        public string RegistrationPlate { get; set; }

        public string EmployeeId { get; set; }
    }
}
