using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeAndParking.Services.Contracts;
using OfficeAndParking.Services.DTOs.ParkingSpotDTOs;

namespace OfficeAndParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpotController : ControllerBase
    {
        private readonly IParkingSpotService _parkingSpotService;

        public ParkingSpotController(IParkingSpotService parkingSpotService)
        {
            _parkingSpotService = parkingSpotService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllParkingSpots()
        {
            var parkingSpots = await _parkingSpotService.GetAllAsync();
            return Ok(parkingSpots);
        }
    }
}
