using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Models;
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
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user is not null)
                {
                    var isUserInRole = await _userManager.IsInRoleAsync(user, request.Role.ToString());
                    if (!isUserInRole && request.Value)
                    {
                        await _userManager.AddToRoleAsync(user, request.Role.ToString());
                    }
                    else if (isUserInRole && !request.Value)
                    {
                        await _userManager.RemoveFromRoleAsync(user, request.Role.ToString());
                    }
                    var userRoles = await _userServices.GetUserRolesAsync(user);
                    return Ok(new UpdateUserRoleRequest.Response(

                        UserRoles: new UserRolesSharedModel(email: user.Email!)
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Roles = userRoles
                        },
                        Message: "Role Updated"));
                }
                return Ok(new UpdateUserRoleRequest.Response(

                    UserRoles: null!,
                    Message: "User was not found"));

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
