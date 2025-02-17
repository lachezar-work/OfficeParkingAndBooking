﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeAndParking.Services.Services;
using OfficeAndParking.Services.Services.Contracts;
using OfficeAndParking.Services.DTOs.EmployeeDTOs;
using Microsoft.AspNetCore.Identity;

namespace OfficeAndParkingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IIdentityService _identityService;

        public EmployeeController(IEmployeeService employeeService, IIdentityService identityService)
        {
            _employeeService = employeeService;
            _identityService = identityService;
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

            return Ok();
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var result = await _employeeService.LoginAsync(model);

            if (!result.Succeeded)
                return Unauthorized(new{message= "Invalid login attempt." });

            return Ok(model);
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(object? empty)
        {
            if (empty != null)
            {
                await _employeeService.Logout();
                return Ok();
            }
            return Unauthorized();
        }
        [HttpGet("current")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var currentUser = new GetCurrentUserDTO()
            {
                EmployeeName = await _identityService.GetCurrentUserFullname(),
                EmployeeTeam = await _identityService.GetCurrentUserTeam()
            };
            return Ok(currentUser);
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
