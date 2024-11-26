using Microsoft.EntityFrameworkCore;
using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.Repositories.Contracts;
using OfficeAndParking.Data;

namespace OfficeAndParking.Services.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee,string>, IEmployeeRepository
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

        public async Task<Employee?> GetWithTeamByIdAsync(string id)
        {
            return await _dbContext.Employees
                .Include(e => e.Team)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}