using Ardalis.ApiEndpoints;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Requests.Admin;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Admin
{
    public class GetUsersRoleEndpoint : EndpointBaseAsync
        .WithRequest<GetUsersRolesRequest>
        .WithResult<GetUsersRolesRequest.Response>
    {
        private readonly IUserServices _userServices;
        public GetUsersRoleEndpoint(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost(GetUsersRolesRequest.EndPointUri)]
        public override async Task<GetUsersRolesRequest.Response> HandleAsync(GetUsersRolesRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var users = _userServices.FindUsers(request.Request.UserSearchPhrase!);
                var usersRoles = await _userServices.GetUsersRolesAsync(users);
                var response = new GetUsersRolesRequest.Response(usersRoles);
                return response;
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }
    }
}
