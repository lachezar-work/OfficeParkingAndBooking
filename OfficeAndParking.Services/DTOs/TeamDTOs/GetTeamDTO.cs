using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeAndParking.Services.DTOs.TeamDTOs
{
    public class GetTeamDTO
    {
        public int Id { get; init; }
        public string ShortName { get; init; }
        public string FullName { get; init; }
    }
}
