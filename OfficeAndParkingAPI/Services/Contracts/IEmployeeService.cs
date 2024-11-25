using OfficeAndParkingAPI.DTOs;
using OfficeAndParking.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace OfficeAndParkingAPI.Services.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<GetEmployeeDTO>> GetAllEmployeesAsync();
        Task<GetEmployeeDTO?> GetEmployeeByIdAsync(string id);
        Task CreateEmployeeAsync(RegisterEmployeeDTO employeeDto);
        Task UpdateEmployeeAsync(string id, UpdateEmployeeDTO employee);
        Task DeleteEmployeeAsync(string id);
        Task<IdentityResult> RegisterAsync(RegisterEmployeeDTO model);
        Task<SignInResult> LoginAsync(LoginDTO model);
    }
}