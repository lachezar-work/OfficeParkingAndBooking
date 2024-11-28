using System.Threading.Tasks;

namespace OfficeAndParking.Services.Services.Contracts
{
    public interface IIdentityService
    {
        string GetCurrentUserId();
        Task<string> GetCurrentUserFullname();
        Task<string> GetCurrentUserTeam();
    }
}
