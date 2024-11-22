using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OfficeAndParkingAPI.DTOs;
using OfficeParkingAndBooking.Data.Models;
using OfficeParkingAndBooking.Data;
using Microsoft.EntityFrameworkCore;

namespace OfficeAndParkingAPI.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly OfficeParkingDbContext _dbContext;

        public EmployeeController(OfficeParkingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetEmployeeDTO>>> GetAllEmployees()
        {
            var employees = await _dbContext.Employees
                .Include(e => e.Team)
                .Select(x => new GetEmployeeDTO(x.Firstname, x.Lastname, x.Team!.FullName))
                .ToListAsync();

            return Ok(employees);
        }

        // GET: api/employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeDTO>> GetEmployeeById(int id)
        {
            var employee = await _dbContext.Employees
                .Include(e => e.Team)
                .AsNoTracking() // To ensure we don't modify anything unintentionally
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            var dto = new GetEmployeeDTO(employee.Firstname, employee.Lastname, employee.Team!.FullName);
            return Ok(dto);
        }

        // PUT: api/employee/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            var existingEmployee = await _dbContext.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.Firstname = employee.Firstname;
            existingEmployee.Lastname = employee.Lastname;
            existingEmployee.TeamId = employee.TeamId;

            await _dbContext.SaveChangesAsync();

            return NoContent(); // Return a 204 status code indicating success without content
        }

        // POST: api/employee
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        // DELETE: api/employee/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

            return NoContent(); // Return a 204 status code indicating the entity was deleted successfully
        }
    }
}
