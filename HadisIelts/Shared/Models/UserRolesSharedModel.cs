using static HadisIelts.Shared.Enums.UserRelatedEnums;

namespace HadisIelts.Shared.Models
{
    public class UserRolesSharedModel
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; init; }
        public List<UserRoleModel>? Roles { get; set; }
        public UserRolesSharedModel(string id, string email)
        {
            Id = id;
            Email = email;
        }
    }
    public class UserRoleModel
    {
        public ApplicationRoles Role { get; set; }
        public bool Value { get; set; }
        public UserRoleModel()
        {

        }
        public UserRoleModel(ApplicationRoles role, bool value)
        {
            Role = role;
            Value = value;
        }
    }
}
