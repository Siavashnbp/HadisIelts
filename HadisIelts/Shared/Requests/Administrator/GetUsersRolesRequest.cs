using MediatR;

namespace HadisIelts.Shared.Requests.Admin
{
    public record GetUsersRolesRequest(UsersRolesRequest Request) : IRequest<GetUsersRolesRequest.Response>
    {
        public const string EndPointUri = "/api/administrator/getRoles";
        public record Response(List<UserRoles> UsersRoles);
    }
    public class UsersRolesRequest
    {
        public string? UserSearchPhrase { get; set; }
        public List<ApplicationRoles>? InRoles { get; set; }
    }
    public class UserRoles
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email { get; set; }
        public List<Tuple<ApplicationRoles, bool>>? Roles { get; set; }
        public string RolesInJson { get; set; }

    }
    public enum ApplicationRoles
    {
        Admin = 0,
        Teacher = 1,
        Student = 2,
    }
}
