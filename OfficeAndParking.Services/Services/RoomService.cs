using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.Contracts;
using OfficeAndParking.Services.Repositories;
using OfficeAndParking.Services.Repositories.Contracts;

namespace OfficeAndParking.Services.Services
{
    public class RoomService:IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _roomRepository.GetAllAsync();
        }
        public async Task<Room> GetRoomById(int id)
        {
            return await _roomRepository.GetByIdAsync(id);
        }
    }
}
