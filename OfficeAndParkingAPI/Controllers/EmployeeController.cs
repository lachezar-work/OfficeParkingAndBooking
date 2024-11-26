using Microsoft.AspNetCore.Mvc;
using OfficeAndParkingAPI.DTOs;
using OfficeAndParkingAPI.Services;
using OfficeAndParkingAPI.Services.Contracts;
using OfficeAndParking.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

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
        [HttpPost("assignrole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole(RegisterEmployeeDTO model)
        {
            var result = await _employeeService.RegisterAsync(model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Registration successful.");
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterEmployeeDTO model)
        {
            var result = await _employeeService.RegisterAsync(model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Registration successful.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var result = await _employeeService.LoginAsync(model);

            if (!result.Succeeded)
                return Unauthorized("Invalid login attempt.");

            return Ok("Login successful.");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetEmployeeDTO>>> GetAllEmployees()
        {
            var employees = await _employeeService
                .GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeDTO>> GetEmployeeById(string id)
        {
            var employee = await _employeeService
                .GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(string id, UpdateEmployeeDTO employee)
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
        public async Task<ActionResult> DeleteEmployee(string id)
        {
            await _employeeService
                .DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}
