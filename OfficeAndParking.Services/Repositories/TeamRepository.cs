using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeAndParking.Data;
using OfficeAndParking.Data.Models;

namespace OfficeAndParking.Services.Repositories
{
    public class TeamRepository:BaseRepository<Team,int>
    {
        public TeamRepository(OfficeParkingDbContext dbContext) : base(dbContext)
        {
        }
    }
}
