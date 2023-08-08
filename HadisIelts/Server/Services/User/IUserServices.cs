using HadisIelts.Server.Models;
using HadisIelts.Shared.Requests.Admin;
using static HadisIelts.Shared.Enums.UserRelatedEnums;

namespace HadisIelts.Server.Services.User
{
    public interface IUserServices
    {
        public List<ApplicationUser> FindUsers(string searchPhrase);
        public Task<List<UserRoles>> GetUsersRolesAsync(List<ApplicationUser> users);
        public Task<List<Tuple<ApplicationRoles, bool>>> GetUserRolesAsync(ApplicationUser user);
    }
}
