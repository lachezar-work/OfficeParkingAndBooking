using Microsoft.EntityFrameworkCore;
using OfficeAndParkingAPI.Repositories.Contracts;
using OfficeAndParking.Data;
using OfficeAndParking.Data.Models;

namespace OfficeAndParkingAPI.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(OfficeParkingDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Employee>> GetAllWithTeamAsync()
        {
            return await _dbContext.Employees
                .Include(e => e.Team)
                .ToListAsync();
        }

        public async Task<Employee?> GetWithTeamByIdAsync(int id)
        {
            return await _dbContext.Employees
                .Include(e => e.Team)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}