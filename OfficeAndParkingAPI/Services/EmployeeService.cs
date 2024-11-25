using OfficeAndParkingAPI.DTOs;
using OfficeAndParkingAPI.Repositories;
using OfficeAndParkingAPI.Repositories.Contracts;
using OfficeAndParkingAPI.Services.Contracts;
using OfficeAndParking.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeAndParkingAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<GetEmployeeDTO>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository
                .GetAllWithTeamAsync();
            return employees.Select(e => new GetEmployeeDTO(e.Firstname, e.Lastname, e.Team?.FullName ?? ""));
        }

        public async Task<GetEmployeeDTO?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetWithTeamByIdAsync(id);
            if (employee == null) return null;

            return new GetEmployeeDTO(employee.Firstname, employee.Lastname, employee.Team?.FullName ?? "");
        }

        public async Task CreateEmployeeAsync(CreateEmployeeDTO employeeDto)
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

        public async Task UpdateEmployeeAsync(int id, UpdateEmployeeDTO employee)
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

        public async Task DeleteEmployeeAsync(int id)
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
