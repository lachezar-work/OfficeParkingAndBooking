using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeAndParking.Services.Services;
using OfficeAndParking.Services.Services.Contracts;
using OfficeAndParking.Services.DTOs.EmployeeDTOs;

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
        public async Task<IActionResult> AssignRole(AssignRoleDTO model)
        {
            await _employeeService.AssignRoleAsync(model);
            return Ok("Role assignment successful.");
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
                return Unauthorized(new{message= "Invalid login attempt." });

            return Ok(model);
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
