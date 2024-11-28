using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.Contracts;
using OfficeAndParking.Services.DTOs.ParkingSpotDTOs;
using OfficeAndParking.Services.Repositories;
using OfficeAndParking.Services.Repositories.Contracts;

namespace OfficeAndParking.Services.Services
{
    public class ParkingSpotService : IParkingSpotService
    {
        private readonly IParkingSpotRepository _parkingSpotRepository;

        public ParkingSpotService(IParkingSpotRepository parkingSpotRepository)
        {
            _parkingSpotRepository = parkingSpotRepository;
        }

        public async Task<IEnumerable<GetParkingSpotDTO>> GetAllAsync()
        {
            var parkingSpots = await _parkingSpotRepository.GetAllAsync();
            return parkingSpots.Select(x => new GetParkingSpotDTO()
            {
                Id = x.Id,
                Number = x.SpotNumber
            });
        }
    }
}
