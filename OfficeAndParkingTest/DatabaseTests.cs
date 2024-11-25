using System.Diagnostics;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OfficeAndParking.Data;
using OfficeAndParking.Data.Models;

namespace OfficeAndParkingTests
{
    [TestClass]
    public class DatabaseTests
    {

        [TestMethod]
        public void CanInsertEmployeeIntoDatabase()
        {
            using var context = SetUpSqLiteMemoryWithOpenConnection();

            var employee = new Employee()
            {
                Firstname = "a",
                Lastname = "b",
                TeamId = 1
            };
            context.Employees.Add(employee);
            Debug.WriteLine($"Before save: {employee.Id}");
            context.SaveChanges();
            Debug.WriteLine($"After save: {employee.Id}");

            Assert.AreNotEqual(0,employee.Id);
        }
        [TestMethod]
        public void InsertEmployeesReturnsCorrectResultNumber()
        {
            using var context = SetUpSqLiteMemoryWithOpenConnection();

            List<Employee> employeesToAdd = new List<Employee>()
            {
                new Employee { Firstname = "a", Lastname = "b", TeamId = 1 },
                new Employee { Firstname = "b", Lastname = "c", TeamId = 1 },
                new Employee { Firstname = "d", Lastname = "e", TeamId = 1 },
                new Employee { Firstname = "f", Lastname = "g", TeamId = 1 },
            };

            var dataLogic = new DataLogic(context);

            Assert.AreEqual(employeesToAdd.Count,dataLogic.ImportEmployees(employeesToAdd));
        }
        [TestMethod]
        public void ChangeTrackerIdentifiesAddedEmployee()
        {
            using var context = SetUpSqLiteMemoryWithOpenConnection();

            var employee = new Employee { Firstname = "a", Lastname = "b", TeamId = 1 };
            context.Employees.Add(employee);

            Assert.AreEqual(EntityState.Added, context.Entry(employee).State);
        }

        private static OfficeParkingDbContext SetUpSqLiteMemoryWithOpenConnection()
        {
            var builder = new DbContextOptionsBuilder<OfficeParkingDbContext>().UseSqlite("Filename=:memory:");
            var context = new OfficeParkingDbContext(builder.Options);
;           context.Database.OpenConnection();
            context.Database.EnsureCreated();
            return context;
        }
    }
}