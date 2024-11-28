using OfficeAndParking.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using OfficeAndParking.Services.DTOs.ParkingSpotDTOs;

namespace OfficeAndParking.Services.Contracts
{
    public interface IParkingSpotService
    {
        Task<IEnumerable<GetParkingSpotDTO>> GetAllAsync();
    }
}
