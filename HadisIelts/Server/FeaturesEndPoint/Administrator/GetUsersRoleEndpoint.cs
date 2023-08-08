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
        [Authorize(Roles = "Administrator")]
        [HttpPost(GetUsersRolesRequest.EndPointUri)]
        public override async Task<GetUsersRolesRequest.Response> HandleAsync(GetUsersRolesRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var searchedUsers = _userServices.FindUsers(request.Request.UserSearchPhrase!);
                if (searchedUsers is not null)
                {
                    if (searchedUsers.Count <= 10)
                    {
                        var usersRoles = await _userServices.GetUsersRolesAsync(searchedUsers);
                        var response = new GetUsersRolesRequest.Response(new UsersRolesResponse
                        {
                            UsersRoles = usersRoles,
                            PageNumber = 0,
                            IsLastPage = true
                        });
                        return response;
                    }
                    int pageRange = (int)Math.Floor(searchedUsers.Count / 10.0);
                    if (request.Request.PageNumber < 0)
                    {
                        var users = searchedUsers.GetRange(0, 10);
                        var usersRoles = await _userServices.GetUsersRolesAsync(searchedUsers);
                        var response = new GetUsersRolesRequest.Response(new UsersRolesResponse
                        {
                            UsersRoles = usersRoles,
                            PageNumber = request.Request.PageNumber,
                            IsLastPage = false
                        });
                        return response;
                    }
                    else if (request.Request.PageNumber < pageRange)
                    {
                        var users = searchedUsers.GetRange(request.Request.PageNumber, 10);
                        var usersRoles = await _userServices.GetUsersRolesAsync(searchedUsers);
                        var response = new GetUsersRolesRequest.Response(new UsersRolesResponse
                        {
                            UsersRoles = usersRoles,
                            PageNumber = request.Request.PageNumber,
                            IsLastPage = false
                        });
                        return response;
                    }
                    else
                    {
                        var range = searchedUsers.Count - (request.Request.PageNumber * 10);
                        var users = searchedUsers.GetRange(pageRange, range);
                        var usersRoles = await _userServices.GetUsersRolesAsync(searchedUsers);
                        var response = new GetUsersRolesRequest.Response(new UsersRolesResponse
                        {
                            UsersRoles = usersRoles,
                            PageNumber = pageRange,
                            IsLastPage = true
                        });
                        return response;
                    }
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
