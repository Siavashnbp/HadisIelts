using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Shared.Requests.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Account
{
    public class RegisterUserEndPoint : EndpointBaseAsync
        .WithRequest<RegisterAccountRequest>
        .WithActionResult<RegisterAccountRequest.Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public RegisterUserEndPoint(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost(RegisterAccountRequest.EndpointUri)]
        public override async Task<ActionResult<RegisterAccountRequest.Response>> HandleAsync(RegisterAccountRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user is null)
                {
                    user = new ApplicationUser
                    {
                        FirstName = string.Empty,
                        LastName = string.Empty,
                        UserName = request.Email,
                        Email = request.Email,
                        EmailConfirmed = true
                    };
                    var result = await _userManager.CreateAsync(user, request.Password);
                    if (result.Succeeded)
                    {
                        return Ok(new RegisterAccountRequest.Response(true));
                    }
                }
                return Ok(new RegisterAccountRequest.Response(false));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
