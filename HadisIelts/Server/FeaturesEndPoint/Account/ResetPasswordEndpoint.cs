using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Shared.Requests.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Account
{
    public class ResetPasswordEndpoint : EndpointBaseAsync
        .WithRequest<ResetPasswordRequest>
        .WithActionResult
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ResetPasswordEndpoint(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost(ResetPasswordRequest.EndpointUri)]
        public override async Task<ActionResult> HandleAsync(ResetPasswordRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
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
