using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeAndParkingAPI.Repositories.Contracts;
using OfficeAndParkingAPI.Repositories;
using OfficeAndParkingAPI.Services;
using OfficeAndParkingAPI.Services.Contracts;
using OfficeAndParking.Data;
using OfficeAndParking.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using OfficeAndParking.Data.Seeder;

namespace OfficeAndParkingAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        ConfigureServices(builder);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        ConfigureMiddleware(app);

        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {

        builder.Services.AddDbContext<OfficeParkingDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("OfficeAndParkingConnection"))
                   .EnableSensitiveDataLogging()
                   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

        builder.Services.AddIdentity<Employee,IdentityRole>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
            .AddEntityFrameworkStores<OfficeParkingDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();

        builder.Services.ConfigureHttpJsonOptions(options =>
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        builder.Services.AddControllers();

        builder.Services.AddAuthentication();
        builder.Services.AddAuthorization();

        // Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    private static void ConfigureMiddleware(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            InitializeDatabase(app);

            // Add Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Seed roles and users
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var userManager = services.GetRequiredService<UserManager<Employee>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            SeedData.Initialize(services, userManager, roleManager).GetAwaiter().GetResult();
        }

        // Common Middleware
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        //app.MapIdentityApi<Employee>();

        // Map Controllers
        app.MapControllers();
    }

    private static void InitializeDatabase(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<OfficeParkingDbContext>();
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }
    }

}
