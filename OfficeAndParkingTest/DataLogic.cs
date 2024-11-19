using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeParkingAndBooking.Data;
using OfficeParkingAndBooking.Data.Models;

namespace OfficeAndParkingTests
{
    public class DataLogic
    {
        private OfficeParkingDbContext context;
        public DataLogic(OfficeParkingDbContext context)
        {
            this.context=context;
        }

        public int ImportEmployees(List<Employee> employeesList)
        {
            foreach (var employee in employeesList)
            {
                this.context.Employees.Add(employee);
            }

            return this.context.SaveChanges();
        }
    }
}
