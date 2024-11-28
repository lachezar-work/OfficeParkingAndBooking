using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.DTOs.CarDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeAndParking.Services.Contracts
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task AddNewCar(AddNewCarDTO model);
    }
}