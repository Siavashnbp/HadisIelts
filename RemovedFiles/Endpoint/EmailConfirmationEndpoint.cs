using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Shared.Requests.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Account
{
    public class EmailConfirmationEndpoint : EndpointBaseAsync
        .WithRequest<ConfirmEmailRequest>
        .WithActionResult
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public EmailConfirmationEndpoint(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost(ConfirmEmailRequest.EndpointUri)]
        public override async Task<ActionResult> HandleAsync(ConfirmEmailRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user != null)
                {
                    var result = await _userManager.ConfirmEmailAsync(user, request.Token);
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
