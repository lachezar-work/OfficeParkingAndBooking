using System.Security.Cryptography.X509Certificates;

namespace OfficeParkingAndBooking.Data
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new OfficeParkingDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

        }
    }
}
