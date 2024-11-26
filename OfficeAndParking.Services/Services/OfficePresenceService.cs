using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.DTOs.OfficePresenceDTOs;
using OfficeAndParking.Services.Exceptions;
using OfficeAndParking.Services.Repositories;
using System.Security.Claims;

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
        // Separate service 
        private string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task AddOfficePresence(AddPresenceDTO model)
        {
            var userId = GetCurrentUserId();
            if (await _presenceRepository.HasPresenceAtDateAsync(model.Date, userId))
            {
                throw new DuplicateEntityException( "You already have presence at this date");
            }

            if (expr)
            {
                
            }
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
        public async Task<IEnumerable<OfficePresence>> GetAllOfficePresencesAsync()
        {
            var officePresences = await _presenceRepository.GetAllWithDetails();
            return officePresences;
        }
        public async Task RemoveOfficePresenceAsync(int id)
        {
            var presenceToDelete = await _presenceRepository.GetByIdAsync(id);
            if (presenceToDelete == null)
            {
                throw new EntityNotFoundException("Office presence not found");
            }

            if (GetCurrentUserId() != presenceToDelete.EmployeeId)
            {
                throw new NonAuthorizedException("You cannot delete that! ");
            }
            await _presenceRepository.DeleteAsync(new OfficePresence()
            {
                Id=id
            });
        }
    }
}
