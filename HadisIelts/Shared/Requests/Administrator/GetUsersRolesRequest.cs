using MediatR;
using static HadisIelts.Shared.Enums.UserRelatedEnums;

namespace HadisIelts.Shared.Requests.Admin
{
    public record GetUsersRolesRequest(UsersRolesRequest Request) : IRequest<GetUsersRolesRequest.Response>
    {
        public const string EndPointUri = "/api/administrator/getRoles";
        public record Response(UsersRolesResponse UsersRolesResponse);
    }
    public class UsersRolesRequest
    {
        public string? UserSearchPhrase { get; set; }
        public int PageNumber { get; set; }
    }
    public class UsersRolesResponse
    {
        public List<UserRoles> UsersRoles { get; set; }
        public int PageNumber { get; set; }
        public bool IsLastPage { get; set; }
    }
    public class UserRoles
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public List<Tuple<ApplicationRoles, bool>>? Roles { get; set; }
    }
}
