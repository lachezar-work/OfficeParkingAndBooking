﻿using Microsoft.AspNetCore.Identity;
using OfficeAndParking.Data.Models;

namespace OfficeAndParking.Data.Seeder
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider,OfficeParkingDbContext context, UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roleNames = new[] { "Admin", "User", "Manager" }; // Example roles
            var employees = new[]
            {
                new
                {
                    Firstname = "Lachezar",
                    Lastname = "Atanasov",
                    Username = "lachezar",
                    TeamId = 1,
                    Password = "123456",
                    Role=roleNames[0]
                },
                new
                {
                    Firstname = "Test",
                    Lastname = "Testov",
                    Username = "test",
                    TeamId = 1,
                    Password = "123456",
                    Role=roleNames[1]
                },
            };

            // Seed Roles
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var role = new IdentityRole() { Name = roleName };
                    await roleManager.CreateAsync(role);
                }
            }

            // Seed Users and presences
            foreach (var userData in employees)
            {
                var user = await userManager.FindByNameAsync(userData.Username);
                if (user == null)
                {
                    user = new Employee()
                    {
                        Email = userData.Username+"@abv.bg",
                        Firstname = userData.Firstname,
                        Lastname = userData.Lastname,
                        TeamId = userData.TeamId,
                        UserName = userData.Username
                    };

                    var createResult = await userManager.CreateAsync(user, userData.Password);
                    context.OfficePresences.AddRange(new List<OfficePresence>()
                    {
                        new OfficePresence()
                        {
                            Date = DateOnly.Parse("26-11-2024"),
                            EmployeeId = user.Id,
                            RoomId = 1,
                            Notes = "Test"
                        },
                        new OfficePresence()
                        {
                            Date = DateOnly.Parse("27-11-2024"),
                            EmployeeId = user.Id,
                            RoomId = 1,
                            Notes = "Test"
                        },
                        new OfficePresence()
                        {
                            Date = DateOnly.Parse("28-11-2024"),
                            EmployeeId = user.Id,
                            RoomId = 1,
                            Notes = "Test"
                        },
                    });
                    context.Cars.AddRange(new List<Car>()
                    {
                        new Car()
                        {
                            Brand = "Audi A3",
                            RegistrationPlate = "CB5768M"+char.ToUpper(user.Firstname[0]),
                            EmployeeId = user.Id
                        }
                    });
                    if (createResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, userData.Role);
                    }
                }
            }
        }
    }

}
