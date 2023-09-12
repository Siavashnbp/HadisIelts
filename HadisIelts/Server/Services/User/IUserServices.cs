using HadisIelts.Server.Models;
using HadisIelts.Shared.Models;
using System.Security.Claims;
using static HadisIelts.Shared.Enums.UserRelatedEnums;

namespace HadisIelts.Server.Services.User
{
    public interface IUserServices
    {
        public List<ApplicationUser> FindUsers(string searchPhrase);
        public Task<List<UserRolesSharedModel>> GetUsersRolesAsync(List<ApplicationUser> users);
        public Task<List<Tuple<ApplicationRoles, bool>>> GetUserRolesAsync(ApplicationUser user);
        public bool IsUserOwnerOrSpecificRoles(List<Claim> claims, List<string> roles, string userID);
        public Task<UserInformationSharedModel> GetUserInformationAsync(string userID);
    }
}
