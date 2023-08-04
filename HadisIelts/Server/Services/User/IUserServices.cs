using HadisIelts.Server.Models;
using HadisIelts.Shared.Requests.Admin;

namespace HadisIelts.Server.Services.User
{
    public interface IUserServices
    {
        public List<ApplicationUser> FindUsers(string searchPhrase);
        public Task<List<UserRoles>> GetUsersRolesAsync(List<ApplicationUser> users);
    }
}
