using OfficeAndParking.Services.DTOs.TeamDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeAndParking.Services.Contracts
{
    public interface ITeamService
    {
        Task<IEnumerable<GetTeamDTO>> GetAllAsync();
    }
}
