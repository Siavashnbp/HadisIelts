using HadisIelts.Shared.Requests.Admin;
using MediatR;
using static HadisIelts.Shared.Enums.UserRelatedEnums;

namespace HadisIelts.Shared.Requests.Administrator
{
    public record UpdateUserRoleRequest(UserNewRole Request) : IRequest<UpdateUserRoleRequest.Response>
    {
        public const string EndPointUri = "/api/administrator/updateUserRole";
        public record Response(UpdatedUserRole UpdatedUserRole);
    }
    public class UserNewRole
    {
        public string Email { get; set; }
        public ApplicationRoles Role { get; set; }
        public bool Value { get; set; }
    }
    public class UpdatedUserRole
    {
        public UserRoles UserRoles { get; set; }
        public string Message { get; set; }
    }
}
