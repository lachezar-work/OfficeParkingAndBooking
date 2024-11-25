using OfficeAndParkingAPI.DTOs;
using OfficeAndParkingAPI.Repositories;
using OfficeAndParkingAPI.Repositories.Contracts;
using OfficeAndParkingAPI.Services.Contracts;
using OfficeAndParking.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OfficeAndParkingAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;

        public EmployeeService(UserManager<Employee> userManager, SignInManager<Employee> signInManager, IEmployeeRepository employeeRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _employeeRepository = employeeRepository;
        }
        public async Task<IdentityResult> RegisterAsync(RegisterEmployeeDTO model)
        {
            var user = new Employee
            {
                UserName = model.Email,
                Email = model.Email,
                Firstname = model.FirstName,
                Lastname = model.LastName,
                TeamId = model.TeamId
            };

            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task<SignInResult> LoginAsync(LoginDTO model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
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

        public async Task CreateEmployeeAsync(RegisterEmployeeDTO employeeDto)
        {
            var employee = new Employee
            {
                Firstname = employeeDto.FirstName,
                Lastname = employeeDto.LastName,
                TeamId = employeeDto.TeamId
            };
            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(string id, UpdateEmployeeDTO employee)
        {
            var employeeToUpdate = await _employeeRepository.GetWithTeamByIdAsync(id);

            if (employeeToUpdate == null) return;

            if (!string.IsNullOrEmpty(employee.FirstName))
                employeeToUpdate.Firstname = employee.FirstName;

            if (!string.IsNullOrEmpty(employee.LastName))
                employeeToUpdate.Lastname = employee.LastName;

            if (employee.TeamId != 0)
                employeeToUpdate.TeamId = employee.TeamId;

            await _employeeRepository.UpdateAsync(employeeToUpdate);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(string id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee != null)
            {
                await _employeeRepository.DeleteAsync(employee);
                await _employeeRepository.SaveChangesAsync();
            }
        }


    }
}
