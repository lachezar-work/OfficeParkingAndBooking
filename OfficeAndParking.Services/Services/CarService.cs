using System.Collections.Generic;
using System.Threading.Tasks;
using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.Contracts;
using OfficeAndParking.Services.DTOs.CarDTOs;
using OfficeAndParking.Services.Repositories.Contracts;

namespace OfficeAndParking.Services.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _carRepository.GetAllAsync();
        }

        public async Task AddNewCar(AddNewCarDTO model)
        {
            await _carRepository.AddAsync(new Car()
            {
                Brand = model.Brand,
                EmployeeId = model.EmployeeId,
                RegistrationPlate = model.RegistrationPlate
            });
            _carRepository.SaveChangesAsync();
        }

    }
}
