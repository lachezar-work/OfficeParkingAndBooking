using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using OfficeAndParking.Data.Models;
using OfficeAndParking.Data;
using OfficeAndParking.Data.Seeder;
using OfficeAndParking.Services.Repositories;
using OfficeAndParking.Services.Repositories.Contracts;
using OfficeAndParking.Services.Services;
using OfficeAndParking.Services.Services.Contracts;
using OfficeAndParkingAPI.Middlewares;
using OfficeAndParking.Services.Contracts;
using OfficeAndParkingAPI.Filters;

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
                    options.User.RequireUniqueEmail = false;
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

        builder.Services.AddScoped<IIdentityService, IdentityService>();

        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        builder.Services.AddScoped<IOfficePresenceRepository, OfficePresenceRepository>();
        builder.Services.AddScoped<IOfficePresenceService, OfficePresenceService>();
        builder.Services.AddScoped<IRoomRepository, RoomRepository>();
        builder.Services.AddScoped<IRoomService, RoomService>();
        builder.Services.AddScoped<ITeamRepository, TeamRepository>();
        builder.Services.AddScoped<ITeamService, TeamService>();
        builder.Services.AddScoped<IParkingSpotRepository, ParkingSpotRepository>();
        builder.Services.AddScoped<IParkingSpotService, ParkingSpotService>();
        builder.Services.AddScoped<ICarRepository, CarRepository>();
        builder.Services.AddScoped<ICarService, CarService>();


        builder.Services.ConfigureHttpJsonOptions(options =>
            options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

        builder.Services.AddControllers(config => config.Filters.Add(new ErrorResultFilter()));

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
            var officeParkingDbContext = services.GetRequiredService<OfficeParkingDbContext>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            SeedData.Initialize(services, officeParkingDbContext,userManager, roleManager).GetAwaiter().GetResult();
        }

        // Common Middleware
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        //app.MapIdentityApi<Employee>();

        // Map Controllers
        app.UseMiddleware<ExceptionMiddleware>();
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
