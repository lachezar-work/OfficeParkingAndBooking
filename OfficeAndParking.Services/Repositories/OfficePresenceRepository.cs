using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.JavaScript;
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
        /// <summary>
        /// Retrieves all OfficePresence records from the database, including related ParkingSpotReservation,
        /// Employee, and Team details.
        /// </summary>
        /// <returns>A list of OfficePresence records with related details.</returns>
        public async Task<IEnumerable<OfficePresence>> GetAllWithDetails()
        {
            return await _dbContext.OfficePresences
                .Include(op => op.ParkingSpotReservation)
                .Include(op => op.OfficeRoom)
                .Include(op => op.Employee)
                .ThenInclude(e => e.Team)
                .ToListAsync();
        }
        public async Task<bool> HasPresenceAtDateAsync(DateOnly date, string employeeId)
        {
            var presence = await _dbContext.OfficePresences
                .Where(op => op.Date == date && op.EmployeeId == employeeId)
                .FirstOrDefaultAsync();

            return presence != null;
        }
        public async Task<int> GetOccupiedSpots(int roomId, DateOnly date)
        {
                var usedSeats = await _dbContext.OfficePresences
                    .Where(op => op.Date == date && op.RoomId == roomId)
                    .CountAsync();
            return usedSeats;
        }

    }
}
