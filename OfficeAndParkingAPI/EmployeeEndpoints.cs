using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using OfficeParkingAndBooking.Data;
using OfficeParkingAndBooking.Data.Models;
namespace OfficeAndParkingAPI;

public static class EmployeeEndpoints
{
    public static void MapEmployeeEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Employee").WithTags(nameof(Employee));

        group.MapGet("/", async (OfficeParkingDbContext db) =>
        {
            return await db.Employees.ToListAsync();
        })
        .WithName("GetAllEmployees")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Employee>, NotFound>> (int id, OfficeParkingDbContext db) =>
        {
            return await db.Employees.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Employee model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetEmployeeById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Employee employee, OfficeParkingDbContext db) =>
        {
            var affected = await db.Employees
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, employee.Id)
                    .SetProperty(m => m.Firstname, employee.Firstname)
                    .SetProperty(m => m.Lastname, employee.Lastname)
                    .SetProperty(m => m.TeamId, employee.TeamId)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateEmployee")
        .WithOpenApi();

        group.MapPost("/", async (Employee employee, OfficeParkingDbContext db) =>
        {
            db.Employees.Add(employee);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Employee/{employee.Id}",employee);
        })
        .WithName("CreateEmployee")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, OfficeParkingDbContext db) =>
        {
            var affected = await db.Employees
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteEmployee")
        .WithOpenApi();
    }
}
