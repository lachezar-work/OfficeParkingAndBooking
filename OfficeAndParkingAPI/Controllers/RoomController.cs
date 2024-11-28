using Microsoft.AspNetCore.Mvc;
using OfficeAndParking.Services.Contracts;
using OfficeAndParking.Services.Services;

namespace OfficeAndParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomService.GetAllAsync();
            return Ok(rooms);
        }
    }
}