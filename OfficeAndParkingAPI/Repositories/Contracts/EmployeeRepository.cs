using OfficeAndParking.Data.Models;

namespace OfficeAndParkingAPI.Repositories.Contracts
{
    public interface IEmployeeRepository : IBaseRepository<Employee,string>
    {
        Task<IEnumerable<Employee>> GetAllWithTeamAsync();
        Task<Employee?> GetWithTeamByIdAsync(string id);
    }
}