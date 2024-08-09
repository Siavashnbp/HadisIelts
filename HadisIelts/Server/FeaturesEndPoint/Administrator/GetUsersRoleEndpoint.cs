using Ardalis.ApiEndpoints;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Requests.Admin;
using Microsoft.AspNetCore.Authorization;
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
        /// <summary>
        /// gets users roles based on search phrase
        /// </summary>
        /// <param name="request">
        /// UserSearchPhrase: a phrase in user email, firstname or last name. null value is handled in service.
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// list of user roles
        /// null if no user is found
        /// </returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost(GetUsersRolesRequest.EndPointUri)]
        public override async Task<GetUsersRolesRequest.Response> HandleAsync(GetUsersRolesRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var searchedUsers = await _userServices.FindUsers(request.UserSearchPhrase!);
                if (searchedUsers is not null)
                {
                    var usersRoles = await _userServices.GetUsersRolesAsync(searchedUsers);
                    return new GetUsersRolesRequest.Response(UsersRoles: usersRoles);
                }
                return null!;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
