using OfficeAndParking.Services.DTOs.EmployeeDTOs;
using OfficeAndParking.Services.DTOs.OfficePresenceDTOs;
using OfficeAndParking.Data.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OfficeAndParking.Services.Contracts
{
    public interface IOfficePresenceService
    {
        Task<GetEmployeeDTO> AddOfficePresence(AddPresenceDTO model);
        Task<IEnumerable<OfficePresence>> GetAllOfficePresencesAsync();
        Task RemoveOfficePresenceAsync(int id);
    }
}
