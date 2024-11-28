using Microsoft.AspNetCore.Mvc;
using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.Contracts;
using OfficeAndParking.Services.DTOs.CarDTOs;
using OfficeAndParking.Services.Repositories;
using OfficeAndParking.Services.Services;

namespace OfficeAndParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCars()
        {
            var cars = await _carService.GetAllAsync();
            return Ok(cars);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddNewCar(AddNewCarDTO car)
        {
            await _carService.AddNewCar(car);
            return Ok();
        }
    }
}