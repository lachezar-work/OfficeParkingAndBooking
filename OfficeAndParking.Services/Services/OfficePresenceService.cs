using Microsoft.AspNetCore.Identity;
using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.Contracts;
using OfficeAndParking.Services.DTOs.EmployeeDTOs;
using OfficeAndParking.Services.DTOs.OfficePresenceDTOs;
using OfficeAndParking.Services.Exceptions;
using OfficeAndParking.Services.Repositories;
using OfficeAndParking.Services.Repositories.Contracts;
using OfficeAndParking.Services.Services.Contracts;

namespace OfficeAndParking.Services.Services
{
    public class OfficePresenceService:IOfficePresenceService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IOfficePresenceRepository _presenceRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IIdentityService _identityService;
        private readonly IEmployeeRepository _employeeRepository;

        public OfficePresenceService(
            UserManager<Employee> userManager,
            IOfficePresenceRepository presenceRepository,
            IRoomRepository roomRepository,
            IIdentityService identityService,
            IEmployeeRepository employeeRepository)
        {
            _userManager = userManager;
            _presenceRepository = presenceRepository;
            _roomRepository = roomRepository;
            _identityService = identityService;
            _employeeRepository = employeeRepository;
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

        public async Task<GetEmployeeDTO> AddOfficePresence(AddPresenceDTO model)
        {
            var date = DateOnly.FromDateTime(model.Date);
            if (await _presenceRepository.HasPresenceAtDateAsync(date, model.EmployeeId))
            {
                throw new DuplicateEntityException("This employee already has presence at this date");
            }

            await ValidateRoomCapacity(model.RoomId, date);

            var officePresenceToAdd = new OfficePresence()
            {
                Date = date,
                EmployeeId = model.EmployeeId,
                RoomId = model.RoomId,
                Notes = model.Notes
            };

            if (model.ParkingSpot!=null && model.ParkingArrivalTime != null && model.ParkingDepartureTime!=null)
            {
                var parsedParkingArrivalTime = TimeOnly.Parse(model.ParkingArrivalTime);
                var parsedParkingDepartureTime = TimeOnly.Parse(model.ParkingDepartureTime);

                officePresenceToAdd.ParkingSpotReservation = new ParkingSpotReservation()
                {
                        ParkingSpot = new ParkingSpot()
                        {
                            SpotNumber = (int)model.ParkingSpot
                        },
                        ReservedFrom = parsedParkingArrivalTime,
                        ReservedUntil = parsedParkingDepartureTime,
                        CarId = model.CarId
                };
            }
            await _presenceRepository.AddAsync(officePresenceToAdd);
            await _presenceRepository.SaveChangesAsync();

            var employee = await _employeeRepository.GetWithTeamByIdAsync(model.EmployeeId);
            var employeeDetails = new GetEmployeeDTO(employee.Id, employee.Firstname, employee.Lastname, employee.Team.FullName);

            return employeeDetails;
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
                throw new NonAuthorizedException("You cannot delete that!");
            }
            await _presenceRepository.DeleteAsync(new OfficePresence()
            {
                Id = id
            });
        }
    }
}
