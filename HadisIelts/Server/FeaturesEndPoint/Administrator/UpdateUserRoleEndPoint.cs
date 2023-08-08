using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Requests.Admin;
using HadisIelts.Shared.Requests.Administrator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Administrator
{
    public class UpdateUserRoleEndpoint : EndpointBaseAsync
        .WithRequest<UpdateUserRoleRequest>
        .WithActionResult<UpdateUserRoleRequest.Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserServices _userServices;
        public UpdateUserRoleEndpoint(UserManager<ApplicationUser> userManager, IUserServices userServices)
        {
            _userManager = userManager;
            _userServices = userServices;
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost(UpdateUserRoleRequest.EndPointUri)]
        public override async Task<ActionResult<UpdateUserRoleRequest.Response>> HandleAsync(UpdateUserRoleRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (request.Request is not null)
                {
                    var admin = User.Claims;
                    var user = await _userManager.FindByEmailAsync(request.Request.Email);
                    if (user is not null)
                    {
                        var isUserInRole = await _userManager.IsInRoleAsync(user, request.Request.Role.ToString());
                        if (!isUserInRole && request.Request.Value)
                        {
                            await _userManager.AddToRoleAsync(user, request.Request.Role.ToString());
                        }
                        else if (isUserInRole && !request.Request.Value)
                        {
                            await _userManager.RemoveFromRoleAsync(user, request.Request.Role.ToString());
                        }
                        var userRoles = await _userServices.GetUserRolesAsync(user);
                        return Ok(new UpdateUserRoleRequest.Response(new UpdatedUserRole
                        {
                            UserRoles = new UserRoles
                            {
                                Email = user.Email,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Roles = userRoles
                            },
                            Message = "Role Updated"
                        })); ;
                    }
                    return Ok(new UpdateUserRoleRequest.Response(new UpdatedUserRole
                    {
                        UserRoles = default,
                        Message = "User was not found"
                    }));
                }
                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
