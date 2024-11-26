using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeAndParking.Services.DTOs.TeamDTOs;
using OfficeAndParking.Services.Repositories;

namespace OfficeAndParking.Services.Services
{
    public class TeamService
    {
        private readonly TeamRepository _teamRepository;

        public TeamService(TeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<GetTeamDTO>> GetAllAsync()
        {
            var teams = await _teamRepository.GetAllAsync();
            return teams.Select(t => new GetTeamDTO
            {
                Id = t.Id,
                ShortName = t.ShortName,
                FullName = t.FullName
            });
        }
    }
}
