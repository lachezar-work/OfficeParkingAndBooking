using OfficeAndParkingAPI.DTOs;
using OfficeAndParking.Data.Models;

namespace OfficeAndParkingAPI.Services.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<GetEmployeeDTO>> GetAllEmployeesAsync();
        Task<GetEmployeeDTO?> GetEmployeeByIdAsync(int id);
        Task CreateEmployeeAsync(CreateEmployeeDTO employeeDto);
        Task UpdateEmployeeAsync(int id, UpdateEmployeeDTO employee);
        Task DeleteEmployeeAsync(int id);
    }
}