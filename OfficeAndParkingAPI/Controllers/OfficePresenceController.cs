using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeAndParking.Services.DTOs.EmployeeDTOs;
using OfficeAndParking.Services.DTOs.OfficePresenceDTOs;
using OfficeAndParking.Services.Services;
using OfficeAndParking.Services.Services.Contracts;

namespace OfficeAndParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficePresenceController : ControllerBase
    {
        private readonly OfficePresenceService _presenceService;
        public OfficePresenceController(OfficePresenceService presenceService)
        {
            _presenceService = presenceService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPresence()
        {
            await _presenceService.GetAllOfficePresencesAsync();
            return Ok();
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> AddOfficePresence(AddPresenceDTO model)
        {
            await _presenceService.AddOfficePresence(model);
            return Ok();
        } 
    }
}
