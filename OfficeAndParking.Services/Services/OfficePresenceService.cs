using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.DTOs.OfficePresenceDTOs;
using OfficeAndParking.Services.Repositories;

namespace OfficeAndParking.Services.Services
{
    public class OfficePresenceService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly OfficePresenceRepository _presenceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OfficePresenceService(UserManager<Employee> userManager, OfficePresenceRepository presenceRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _presenceRepository = presenceRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task AddOfficePresence(AddPresenceDTO model)
        {
            var userId = GetCurrentUserId();

            var officePresenceToAdd = new OfficePresence()
            {
                Date = model.Date,
                EmployeeId = userId,
                RoomId = model.RoomId,
                ParkingSpotReservationId = model.ParkingSpot,
                Notes = model.Notes
            };
            await _presenceRepository.AddAsync(officePresenceToAdd);
            await _presenceRepository.SaveChangesAsync();
        }
        public async Task<IEnumerable<GetOfficePresenceDTO>> GetAllOfficePresencesAsync()
        {
            var officePresences = await _presenceRepository.GetAllWithParkingSpots();
            return officePresences.Select(x => new GetOfficePresenceDTO()
            {
                Date = x.Date,
                EmployeeName = $"{x.Employee.Firstname} {x.Employee.Lastname}",
                EmployeeTeam = x.Employee.Team.FullName,
                RoomId = x.RoomId,
                ParkingSpot = x.ParkingSpotReservationId,
                ParkingArrivalTime = x.ParkingSpotReservation?.ReservedFrom,
                ParkingDepartureTime = x.ParkingSpotReservation?.ReservedUntil,
                Notes = x.Notes
            });
        }

    }
}
