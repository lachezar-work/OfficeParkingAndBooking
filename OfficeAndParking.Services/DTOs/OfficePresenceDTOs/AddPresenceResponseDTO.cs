using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeAndParking.Services.DTOs.OfficePresenceDTOs
{
    public class AddPresenceResponseDTO
    {
        public string EmployeeName { get; set; }
        public string EmployeeTeam { get; set; }
        public string ParsedDate { get; set; }
    }
}
