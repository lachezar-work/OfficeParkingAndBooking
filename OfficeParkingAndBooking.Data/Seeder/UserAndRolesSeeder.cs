using Microsoft.AspNetCore.Identity;
using OfficeAndParking.Data.Models;
using System;

namespace OfficeAndParking.Data.Seeder
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, OfficeParkingDbContext context, UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            var employeeIds = new List<string>();
            var carBrands = new[] { "Audi A3", "BMW 320i", "Mercedes C200", "Volkswagen Golf", "Toyota Corolla" };
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
                new { Firstname = "John", Lastname = "Doe", Username = "john.doe", TeamId = 3, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Jane", Lastname = "Smith", Username = "jane.smith", TeamId = 5, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Michael", Lastname = "Johnson", Username = "michael.johnson", TeamId = 2, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Emily", Lastname = "Davis", Username = "emily.davis", TeamId = 7, Password = "123456", Role = roleNames[1] },
                new { Firstname = "David", Lastname = "Brown", Username = "david.brown", TeamId = 1, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Sarah", Lastname = "Wilson", Username = "sarah.wilson", TeamId = 4, Password = "123456", Role = roleNames[1] },
                new { Firstname = "James", Lastname = "Taylor", Username = "james.taylor", TeamId = 6, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Jessica", Lastname = "Anderson", Username = "jessica.anderson", TeamId = 8, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Robert", Lastname = "Thomas", Username = "robert.thomas", TeamId = 3, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Laura", Lastname = "Jackson", Username = "laura.jackson", TeamId = 5, Password = "123456", Role = roleNames[1] },
                new { Firstname = "William", Lastname = "White", Username = "william.white", TeamId = 2, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Olivia", Lastname = "Harris", Username = "olivia.harris", TeamId = 7, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Charles", Lastname = "Martin", Username = "charles.martin", TeamId = 1, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Sophia", Lastname = "Thompson", Username = "sophia.thompson", TeamId = 4, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Daniel", Lastname = "Garcia", Username = "daniel.garcia", TeamId = 6, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Emma", Lastname = "Martinez", Username = "emma.martinez", TeamId = 8, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Matthew", Lastname = "Robinson", Username = "matthew.robinson", TeamId = 3, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Ava", Lastname = "Clark", Username = "ava.clark", TeamId = 5, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Joseph", Lastname = "Rodriguez", Username = "joseph.rodriguez", TeamId = 2, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Mia", Lastname = "Lewis", Username = "mia.lewis", TeamId = 7, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Christopher", Lastname = "Lee", Username = "christopher.lee", TeamId = 1, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Isabella", Lastname = "Walker", Username = "isabella.walker", TeamId = 4, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Andrew", Lastname = "Hall", Username = "andrew.hall", TeamId = 6, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Amelia", Lastname = "Allen", Username = "amelia.allen", TeamId = 8, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Joshua", Lastname = "Young", Username = "joshua.young", TeamId = 3, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Abigail", Lastname = "Hernandez", Username = "abigail.hernandez", TeamId = 5, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Ethan", Lastname = "King", Username = "ethan.king", TeamId = 2, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Charlotte", Lastname = "Wright", Username = "charlotte.wright", TeamId = 7, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Alexander", Lastname = "Lopez", Username = "alexander.lopez", TeamId = 1, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Harper", Lastname = "Hill", Username = "harper.hill", TeamId = 4, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Benjamin", Lastname = "Scott", Username = "benjamin.scott", TeamId = 6, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Evelyn", Lastname = "Green", Username = "evelyn.green", TeamId = 8, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Lucas", Lastname = "Adams", Username = "lucas.adams", TeamId = 3, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Avery", Lastname = "Baker", Username = "avery.baker", TeamId = 5, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Henry", Lastname = "Gonzalez", Username = "henry.gonzalez", TeamId = 2, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Ella", Lastname = "Nelson", Username = "ella.nelson", TeamId = 7, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Sebastian", Lastname = "Carter", Username = "sebastian.carter", TeamId = 1, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Grace", Lastname = "Mitchell", Username = "grace.mitchell", TeamId = 4, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Jack", Lastname = "Perez", Username = "jack.perez", TeamId = 6, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Chloe", Lastname = "Roberts", Username = "chloe.roberts", TeamId = 8, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Samuel", Lastname = "Turner", Username = "samuel.turner", TeamId = 3, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Lily", Lastname = "Phillips", Username = "lily.phillips", TeamId = 5, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Owen", Lastname = "Campbell", Username = "owen.campbell", TeamId = 2, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Zoey", Lastname = "Parker", Username = "zoey.parker", TeamId = 7, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Logan", Lastname = "Evans", Username = "logan.evans", TeamId = 1, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Hannah", Lastname = "Edwards", Username = "hannah.edwards", TeamId = 4, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Mason", Lastname = "Collins", Username = "mason.collins", TeamId = 6, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Aria", Lastname = "Stewart", Username = "aria.stewart", TeamId = 8, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Elijah", Lastname = "Sanchez", Username = "elijah.sanchez", TeamId = 3, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Scarlett", Lastname = "Morris", Username = "scarlett.morris", TeamId = 5, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Aiden", Lastname = "Rogers", Username = "aiden.rogers", TeamId = 2, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Layla", Lastname = "Reed", Username = "layla.reed", TeamId = 7, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Ethan", Lastname = "Cook", Username = "ethan.cook", TeamId = 1, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Mila", Lastname = "Morgan", Username = "mila.morgan", TeamId = 4, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Jacob", Lastname = "Bell", Username = "jacob.bell", TeamId = 6, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Sofia", Lastname = "Murphy", Username = "sofia.murphy", TeamId = 8, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Lucas", Lastname = "Bailey", Username = "lucas.bailey", TeamId = 3, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Avery", Lastname = "Rivera", Username = "avery.rivera", TeamId = 5, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Henry", Lastname = "Cooper", Username = "henry.cooper", TeamId = 2, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Ella", Lastname = "Richardson", Username = "ella.richardson", TeamId = 7, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Sebastian", Lastname = "Cox", Username = "sebastian.cox", TeamId = 1, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Grace", Lastname = "Howard", Username = "grace.howard", TeamId = 4, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Jack", Lastname = "Ward", Username = "jack.ward", TeamId = 6, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Chloe", Lastname = "Torres", Username = "chloe.torres", TeamId = 8, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Samuel", Lastname = "Peterson", Username = "samuel.peterson", TeamId = 3, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Lily", Lastname = "Gray", Username = "lily.gray", TeamId = 5, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Owen", Lastname = "Ramirez", Username = "owen.ramirez", TeamId = 2, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Zoey", Lastname = "James", Username = "zoey.james", TeamId = 7, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Logan", Lastname = "Watson", Username = "logan.watson", TeamId = 1, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Hannah", Lastname = "Brooks", Username = "hannah.brooks", TeamId = 4, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Mason", Lastname = "Kelly", Username = "mason.kelly", TeamId = 6, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Aria", Lastname = "Sanders", Username = "aria.sanders", TeamId = 8, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Elijah", Lastname = "Price", Username = "elijah.price", TeamId = 3, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Scarlett", Lastname = "Bennett", Username = "scarlett.bennett", TeamId = 5, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Aiden", Lastname = "Wood", Username = "aiden.wood", TeamId = 2, Password = "123456", Role = roleNames[1] },
                new { Firstname = "Layla", Lastname = "Barnes", Username = "layla.barnes", TeamId = 7, Password = "123456", Role = roleNames[1] }  };

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
                        Email = userData.Username + "@abv.bg",
                        Firstname = userData.Firstname,
                        Lastname = userData.Lastname,
                        TeamId = userData.TeamId,
                        UserName = userData.Username
                    };

                    var createResult = await userManager.CreateAsync(user, userData.Password);
                    
                    if (createResult.Succeeded)
                    {
                        employeeIds.Add(user.Id);
                        await userManager.AddToRoleAsync(user, userData.Role);
                    }
                }
            }
            await context.SaveChangesAsync();
            var random = new Random();
            var selectedEmployeeIds = employeeIds.OrderBy(x => random.Next()).Take(10).ToList();
            var startDate = new DateTime(2024, 11, 30);
            var endDate = new DateTime(2024, 12, 10);

            // Seed OfficePresences
            foreach (var employeeId in selectedEmployeeIds)
            {
                var randomDate = startDate.AddDays(random.Next((endDate - startDate).Days + 1));
                var randomRoomId = random.Next(1, 3);
                var randomNotes = $"Note {random.Next(1, 100)}";

                context.OfficePresences.Add(new OfficePresence()
                {
                    Date = DateOnly.FromDateTime(randomDate),
                    EmployeeId = employeeId,
                    RoomId = randomRoomId,
                    Notes = randomNotes
                });
            }

            await context.SaveChangesAsync();
            // Seed Cars
            foreach (var employeeId in selectedEmployeeIds)
            {

                var randomBrand = carBrands[random.Next(carBrands.Length)];
                var registrationPlate = $"CB{random.Next(1000, 9999)}{(char)random.Next(65,90)}{(char)random.Next(65, 90)}";

                context.Cars.Add(
                    new Car()
                    {
                        Brand = randomBrand,
                        RegistrationPlate = registrationPlate,
                        EmployeeId = employeeId
                    });
            }

            await context.SaveChangesAsync();
        }
    }

}
