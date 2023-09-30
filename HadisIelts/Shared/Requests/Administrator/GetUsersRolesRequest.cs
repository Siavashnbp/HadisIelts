using HadisIelts.Shared.Models;
using MediatR;
namespace HadisIelts.Shared.Requests.Admin
{
    public record GetUsersRolesRequest(string? UserSearchPhrase)
        : IRequest<GetUsersRolesRequest.Response>
    {
        public const string EndPointUri = "/api/administrator/getRoles";
        public record Response(List<UserRolesSharedModel>? UsersRoles) : ServerResponse;
    }

}
