using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OfficeAndParking.Data;
using OfficeAndParking.Data.Models;

namespace OfficeAndParking.Services.Repositories
{
    public class OfficePresenceRepository : BaseRepository<OfficePresence, int>
    {
        public OfficePresenceRepository(OfficeParkingDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<OfficePresence>> GetAllWithParkingSpots()
        {
            return await _dbContext.OfficePresences
                .Include(op => op.ParkingSpotReservation)
                .ToListAsync();
        }
    }
}
