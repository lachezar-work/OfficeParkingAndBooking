using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using OfficeAndParking.Data.Models;
using OfficeAndParking.Services.Repositories;
using OfficeAndParking.Services.Repositories.Contracts;
using OfficeAndParking.Services.Services.Contracts;

namespace OfficeAndParking.Services.Services
{
    public class IdentityService:IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly Employee currentEmployee;
        public IdentityService(IHttpContextAccessor httpContextAccessor, IEmployeeRepository employeeRepository, ITeamRepository teamRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _employeeRepository = employeeRepository;
            _teamRepository = teamRepository;
            currentEmployee = _employeeRepository.GetByIdAsync(GetCurrentUserId()).GetAwaiter().GetResult();
        }
        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<string> GetCurrentUserFullname()
        {
            return currentEmployee.Firstname+" "+currentEmployee.Lastname;
        }
        public async Task<string> GetCurrentUserTeam()
        {
            var currentUserTeam = await _teamRepository.GetByIdAsync(currentEmployee.TeamId);
            return currentUserTeam.FullName;
        }
    }
}
