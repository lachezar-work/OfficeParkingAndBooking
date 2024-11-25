﻿using OfficeAndParking.Data.Models;

namespace OfficeAndParkingAPI.Repositories.Contracts
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllWithTeamAsync();
        Task<Employee?> GetWithTeamByIdAsync(int id);
    }
}