using System.ComponentModel.DataAnnotations;
using OfficeAndParkingAPI.Common;

namespace OfficeAndParking.Data.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string RegistrationPlate { get; set; }

        public string EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }

        public virtual ParkingSpotReservation? ParkingSpotReservation { get; set; }
    }
}
