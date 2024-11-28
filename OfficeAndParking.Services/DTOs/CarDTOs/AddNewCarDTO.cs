using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeAndParking.Services.DTOs.CarDTOs
{
    public class AddNewCarDTO
    {
        public string Brand { get; set; }
        public string RegistrationPlate { get; set; }
        public string EmployeeId { get; set; }
    }
}
