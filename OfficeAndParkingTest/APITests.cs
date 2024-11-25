using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using OfficeAndParkingAPI;
using OfficeAndParkingAPI.DTOs;
using OfficeAndParking.Data.Models;

namespace OfficeAndParkingTests
{
    [TestClass]
    public class APITests
    {
        [TestMethod]
        public async Task ApiIsRunning()
        {
            await using var application = new WebApplicationFactory<Program>();
            using var client = application.CreateClient();

            var response = await client.GetStringAsync("/weatherforecast");

            Assert.AreEqual("[{\"date", response.Substring(0, 7));
        }

        [TestMethod]
        public async Task CanRetrieveAnEmployeeDTO()
        {
            await using var application = new CustomWebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var employeeDTO = await client.GetFromJsonAsync<GetEmployeeDTO>("/api/employee/1");
            Assert.IsInstanceOfType(employeeDTO,typeof(GetEmployeeDTO));
        }
        [TestMethod]
        public async Task CanInsertAnEmployee()
        {
            var employeeToAdd = new Employee()
            {
                Firstname = "Test",
                Lastname = "Testov",
                TeamId = 1
            };
            await using var application = new CustomWebApplicationFactory<Program>();
            using var client = application.CreateClient();
            var response = await client.PostAsJsonAsync("/api/employee/", employeeToAdd);
            var employee = await response.Content.ReadFromJsonAsync<Employee>();
            Assert.AreEqual(response.StatusCode,HttpStatusCode.Created);
            Assert.AreNotEqual(0,employee?.Id);
        }
    }
}
