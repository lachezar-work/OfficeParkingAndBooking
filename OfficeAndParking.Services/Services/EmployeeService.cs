using Microsoft.AspNetCore.Identity;
using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.Repositories.Contracts;
using OfficeAndParking.Services.Services.Contracts;
using OfficeAndParking.Services.DTOs.EmployeeDTOs;

namespace OfficeAndParking.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Employee> _signInManager;

        public EmployeeService(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager, SignInManager<Employee> signInManager, IEmployeeRepository employeeRepository) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _employeeRepository = employeeRepository;
            _roleManager = roleManager;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterEmployeeDTO model)
        {
            var user = new Employee
            {
                UserName = model.Username,
                Email = model.Username,
                Firstname = model.FirstName,
                Lastname = model.LastName,
                TeamId = model.TeamId
            };

            return await _userManager.CreateAsync(user, model.Password);
        }
        public async Task<SignInResult> LoginAsync(LoginDTO model)
        {
            return await _signInManager
                .PasswordSignInAsync(model.Username, model.Password, false, false);
        }
        public async Task<IEnumerable<GetEmployeeDTO>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository
                .GetAllWithTeamAsync();
            return employees.Select(e => new GetEmployeeDTO(e.Id,e.Firstname, e.Lastname, e.Team?.FullName ?? ""));
        }
        public async Task<GetEmployeeDTO?> GetEmployeeByIdAsync(string id)
        {
            var employee = await _employeeRepository.GetWithTeamByIdAsync(id);
            if (employee == null) return null;

            return new GetEmployeeDTO(employee.Id, employee.Firstname, employee.Lastname, employee.Team?.FullName ?? "");
        }

        public async Task AssignRoleAsync(AssignRoleDTO model)
        {
            var roleExists = await _roleManager.RoleExistsAsync(model.RoleName);
            if (!roleExists)
            {
                throw new InvalidOperationException($"Role does not exist!");
            }

            var user = await _employeeRepository.GetByIdAsync(model.EmployeeId);

            if (user == null)
            {
                throw new KeyNotFoundException($"Employee with ID {model.EmployeeId} not found.");
            }

            var isInRole = await _userManager.IsInRoleAsync(user, model.RoleName);
            if (isInRole)
            {
                throw new InvalidOperationException($"User already in this role.");
            }

            await _userManager.RemoveFromRolesAsync(user, await _userManager.GetRolesAsync(user));
            await _userManager.AddToRoleAsync(user, model.RoleName);
        }

        public async Task UpdateEmployeeAsync(string id, UpdateEmployeeDTO employee)
        {
            var employeeToUpdate = await _employeeRepository.GetByIdAsync(id);

            if (employeeToUpdate == null) return;

            if (!string.IsNullOrEmpty(employee.FirstName))
                employeeToUpdate.Firstname = employee.FirstName;

            if (!string.IsNullOrEmpty(employee.LastName))
                employeeToUpdate.Lastname = employee.LastName;

            if (!string.IsNullOrEmpty(employee.Username))
            {
                employeeToUpdate.UserName = employee.Username;
                employeeToUpdate.Email = employee.Username;
            }
                

            if (employee.TeamId != 0)
                employeeToUpdate.TeamId = employee.TeamId;

            if (!string.IsNullOrEmpty(employee.Password))
                employeeToUpdate.PasswordHash = _userManager
                    .PasswordHasher
                    .HashPassword(employeeToUpdate,employee.Password);

            await _employeeRepository.UpdateAsync(employeeToUpdate);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(string id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee != null)
            {
                await _userManager.DeleteAsync(employee);
            }
        }


    }
}
