using static HadisIelts.Shared.Enums.UserRelatedEnums;

namespace HadisIelts.Shared.Models
{
    public class UserRolesSharedModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; init; }
        public List<Tuple<ApplicationRoles, bool>>? Roles { get; set; }
        public UserRolesSharedModel(string email)
        {
            Email = email;
        }
    }
}
