﻿using System;
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
        public DateTime Date { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        public int CarId { get; set; }
        public int? ParkingSpot { get; set; }
        public string? ParkingArrivalTime { get; set; }
        public string? ParkingDepartureTime { get; set; }
        public string? Notes { get; set; }
    }
}
