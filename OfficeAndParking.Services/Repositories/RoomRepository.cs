using OfficeAndParking.Data;
using OfficeAndParking.Data.Models;

namespace OfficeAndParking.Services.Repositories
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(OfficeParkingDbContext context) : base(context)
        {
        }
    }
}