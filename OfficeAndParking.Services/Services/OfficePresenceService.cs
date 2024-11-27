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
        private readonly RoomRepository _roomRepository;
        private readonly IdentityService _identityService;

        public OfficePresenceService(
            UserManager<Employee> userManager, 
            OfficePresenceRepository presenceRepository, 
            RoomRepository roomRepository, 
            IdentityService identityService)
        {
            _userManager = userManager;
            _presenceRepository = presenceRepository;
            _roomRepository = roomRepository;
            _identityService = identityService;
        }
        private async Task ValidateRoomCapacity(int roomId, DateOnly date)
        {
            var room = await _roomRepository.GetByIdAsync(roomId);
            var occupiedSpots = await _presenceRepository.GetOccupiedSpots(roomId, date);

            if (room.RoomCapacity <= occupiedSpots)
            {
                throw new InvalidOperationException("No more space in that room!");
            }
        }
        public async Task AddOfficePresence(AddPresenceDTO model)
        {
            var userId = _identityService.GetCurrentUserId();
            if (await _presenceRepository.HasPresenceAtDateAsync(model.Date, userId))
            {
                throw new DuplicateEntityException( "You already have presence at this date");
            }

            await ValidateRoomCapacity(model.RoomId, model.Date);

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

            if (_identityService.GetCurrentUserId() != presenceToDelete.EmployeeId)
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
