using OfficeAndParking.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeAndParking.Services.Repositories.Contracts
{
    public interface IOfficePresenceRepository : IBaseRepository<OfficePresence, int>
    {
        Task<IEnumerable<OfficePresence>> GetAllWithDetails();
        Task<bool> HasPresenceAtDateAsync(DateOnly date, string employeeId);
        Task<int> GetOccupiedSpots(int roomId, DateOnly date);
    }
}
