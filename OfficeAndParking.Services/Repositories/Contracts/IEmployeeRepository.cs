using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.Repositories.Contracts;

namespace OfficeAndParking.Services.Repositories.Contracts
{
    public interface IEmployeeRepository : IBaseRepository<Employee,string>
    {
        Task<IEnumerable<Employee>> GetAllWithTeamAsync();
        Task<Employee?> GetWithTeamByIdAsync(string id);
    }
}