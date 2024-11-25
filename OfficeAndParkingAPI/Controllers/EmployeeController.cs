using Microsoft.AspNetCore.Mvc;
using OfficeAndParkingAPI.DTOs;
using OfficeAndParkingAPI.Services;
using OfficeAndParkingAPI.Services.Contracts;
using OfficeAndParking.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeAndParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetEmployeeDTO>>> GetAllEmployees()
        {
            var employees = await _employeeService
                .GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeDTO>> GetEmployeeById(int id)
        {
            var employee = await _employeeService
                .GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployee(CreateEmployeeDTO employeeDto)
        {
            await _employeeService
                .CreateEmployeeAsync(employeeDto);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeDto.TeamId }, employeeDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, UpdateEmployeeDTO employee)
        {
            try
            {
                await _employeeService
                    .UpdateEmployeeAsync(id, employee);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            await _employeeService
                .DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}
