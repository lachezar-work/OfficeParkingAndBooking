using Microsoft.AspNetCore.Identity;
using OfficeAndParking.Data.Models;

namespace OfficeAndParking.Data.Seeder
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roleNames = new[] { "Admin", "User", "Manager" }; // Example roles
            var employees = new[]
            {
                new
                {
                    Firstname = "Lachezar",
                    Lastname = "Atanasov",
                    Email = "lachezar.atanasov@leadconsult.eu",
                    TeamId = 1,
                    Password = "123456",
                    Role=roleNames[0]
                },
                new
                {
                    Firstname = "Test",
                    Lastname = "Testov",
                    Email = "test@leadconsult.eu",
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

            // Seed Users
            foreach (var userData in employees)
            {
                var user = await userManager.FindByEmailAsync(userData.Email);
                if (user == null)
                {
                    user = new Employee()
                    {
                        Email = userData.Email,
                        Firstname = userData.Firstname,
                        Lastname = userData.Lastname,
                        TeamId = userData.TeamId,
                        UserName = userData.Email
                    };

                    var createResult = await userManager.CreateAsync(user, userData.Password);
                    if (createResult.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, userData.Role);
                    }
                }
            }
        }
    }

}
