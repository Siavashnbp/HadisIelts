using HadisIelts.Server.Data;
using HadisIelts.Server.Models;
using HadisIelts.Shared.Models;
using System.Security.Claims;

namespace HadisIelts.Server.Services.User
{
    public interface IUserServices
    {
        public List<ApplicationUser> FindUsers(string searchPhrase);
        public Task<List<UserRolesSharedModel>> GetUsersRolesAsync(List<ApplicationUser> users);
        public Task<List<UserRoleModel>> GetUserRolesAsync(ApplicationUser user);
        public bool IsUserOwnerOrSpecificRoles(List<Claim> claims, List<string> roles, string userId);
        public Task<UserInformationSharedModel> GetUserInformationAsync(string userId);
        public string GetUserIdFromClaims(List<Claim> claims);
        public bool HasWritingCorrectionPending(ApplicationDbContext dbContext, string userId);
    }
}
