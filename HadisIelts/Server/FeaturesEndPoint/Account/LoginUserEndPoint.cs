using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Shared.Requests.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Account
{
    public class LoginUserEndPoint : EndpointBaseAsync
        .WithRequest<AccountLoginRequest>
        .WithActionResult<AccountLoginRequest.Response>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginUserEndPoint> _logger;
        public LoginUserEndPoint(SignInManager<ApplicationUser> signInManager
            , ILogger<LoginUserEndPoint> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }
        [HttpPost(AccountLoginRequest.EndPointUri)]
        public override async Task<ActionResult<AccountLoginRequest.Response>> HandleAsync(AccountLoginRequest request, CancellationToken cancellationToken = default)
        {
            var userTest = await _signInManager.UserManager.FindByEmailAsync(request.Request.Email);
            var result = await _signInManager.PasswordSignInAsync(
                userName: request.Request.Email,
                password: request.Request.Password,
                isPersistent: request.Request.KeepSignedIn,
                lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
