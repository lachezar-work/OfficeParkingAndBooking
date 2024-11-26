using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeAndParking.Services.DTOs.OfficePresenceDTOs
{
    public class AddPresenceDTO
    {
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public int RoomId { get; set; }
        public int? ParkingSpot { get; set; }
        public TimeOnly? ParkingArrivalTime { get; set; }
        public TimeOnly? ParkingDepartureTime { get; set; }
        public string? Notes { get; set; }
    }
}
