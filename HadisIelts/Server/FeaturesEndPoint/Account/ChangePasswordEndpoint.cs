using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Requests.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Account
{
    public class ChangePasswordEndpoint : EndpointBaseAsync
        .WithRequest<ChangePasswordRequest>
        .WithActionResult
    {
        private readonly IUserServices _userServices;
        private readonly UserManager<ApplicationUser> _userManager;
        public ChangePasswordEndpoint(IUserServices userServices,
            UserManager<ApplicationUser> userManager)
        {
            _userServices = userServices;
            _userManager = userManager;
        }
        [Authorize]
        [HttpPost(ChangePasswordRequest.EndpointUri)]
        public override async Task<ActionResult> HandleAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var userId = _userServices.GetUserIdFromClaims(User.Claims.ToList());
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }
                return Conflict();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
