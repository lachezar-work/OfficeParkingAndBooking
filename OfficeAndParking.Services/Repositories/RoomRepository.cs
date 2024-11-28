using OfficeAndParking.Data;
using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.Repositories.Contracts;

namespace OfficeAndParking.Services.Repositories
{
    public class RoomRepository : BaseRepository<Room,int>, IRoomRepository
    {
        public RoomRepository(OfficeParkingDbContext context) : base(context)
        {
        }
    }
}