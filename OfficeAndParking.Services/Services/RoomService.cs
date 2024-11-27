using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.Repositories;

namespace OfficeAndParking.Services.Services
{
    public class RoomService
    {
        private readonly RoomRepository _roomRepository;

        public RoomService(RoomRepository roomRepository)
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
