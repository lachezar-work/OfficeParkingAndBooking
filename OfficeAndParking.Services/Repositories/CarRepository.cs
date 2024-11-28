using OfficeAndParking.Data;
using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.Repositories.Contracts;

namespace OfficeAndParking.Services.Repositories
{
    public class CarRepository : BaseRepository<Car, int>, ICarRepository
    {
        public CarRepository(OfficeParkingDbContext dbContext) : base(dbContext)
        {
        }
    }
}