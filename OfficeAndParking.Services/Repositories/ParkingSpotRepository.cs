using OfficeAndParking.Data;
using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.Repositories.Contracts;

namespace OfficeAndParking.Services.Repositories
{
    public class ParkingSpotRepository : BaseRepository<ParkingSpot,int>, IParkingSpotRepository
    {
        public ParkingSpotRepository(OfficeParkingDbContext context) : base(context)
        {
        }
    }
}