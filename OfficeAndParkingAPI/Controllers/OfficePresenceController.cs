using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeAndParking.Services.Contracts;
using OfficeAndParking.Services.DTOs.OfficePresenceDTOs;
using OfficeAndParking.Services.Services;

namespace OfficeAndParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficePresenceController : ControllerBase
    {
        private readonly IOfficePresenceService _presenceService;
        public OfficePresenceController(IOfficePresenceService presenceService)
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
                EmployeeId = x.Employee.Id,
                RoomNumber = x.OfficeRoom.Number,
                ParkingSpot = x.ParkingSpotReservationId,
                ParkingArrivalTime = x.ParkingSpotReservation?.ReservedFrom,
                ParkingDepartureTime = x.ParkingSpotReservation?.ReservedUntil,
                Notes = x.Notes
            }));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddOfficePresence(AddPresenceDTO model)
        {
            var employeeDto = await _presenceService.AddOfficePresence(model);
            return Ok(employeeDto);
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveOfficePresence(int id)
        {
            await _presenceService.RemoveOfficePresenceAsync(id);
            return Ok();
        }
    }
}
