using OfficeAndParking.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OfficeAndParking.Services.Contracts
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room> GetRoomById(int id);
    }
}
