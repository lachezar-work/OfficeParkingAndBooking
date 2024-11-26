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
            var presences = await _presenceService.GetAllOfficePresencesAsync();
            return Ok(presences.Select(x => new GetOfficePresenceDTO()
            {
                Date = x.Date,
                EmployeeName = $"{x.Employee.Firstname} {x.Employee.Lastname}",
                EmployeeTeam = x.Employee.Team.FullName,
                RoomId = x.RoomId,
                ParkingSpot = x.ParkingSpotReservationId,
                ParkingArrivalTime = x.ParkingSpotReservation?.ReservedFrom,
                ParkingDepartureTime = x.ParkingSpotReservation?.ReservedUntil,
                Notes = x.Notes
            }));
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
