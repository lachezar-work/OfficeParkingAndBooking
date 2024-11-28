using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeAndParking.Services.Contracts;
using OfficeAndParking.Services.DTOs.TeamDTOs;
using OfficeAndParking.Services.Repositories;
using OfficeAndParking.Services.Repositories.Contracts;

namespace OfficeAndParking.Services.Services
{
    public class TeamService:ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
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
