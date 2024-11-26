using Microsoft.AspNetCore.Mvc;
using OfficeAndParking.Services.Repositories;
using OfficeAndParking.Services.Services;

namespace OfficeAndParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly TeamService _teamService;

        public TeamController(TeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            var teams = await _teamService.GetAllAsync();
            return Ok(teams);
        }
    }
}