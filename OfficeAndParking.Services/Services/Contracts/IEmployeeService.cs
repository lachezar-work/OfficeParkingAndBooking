using Microsoft.AspNetCore.Identity;
using OfficeAndParkingAPI.Services.DTOs;

namespace OfficeAndParking.Services.Services.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<GetEmployeeDTO>> GetAllEmployeesAsync();
        Task<GetEmployeeDTO?> GetEmployeeByIdAsync(string id);
        Task AssignRoleAsync(AssignRoleDTO model);
        Task UpdateEmployeeAsync(string id, UpdateEmployeeDTO employee);
        Task DeleteEmployeeAsync(string id);
        Task<IdentityResult> RegisterAsync(RegisterEmployeeDTO model);
        Task<SignInResult> LoginAsync(LoginDTO model);
    }
}