using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Shared.Requests.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Account
{
    public class RegisterUserEndPoint : EndpointBaseAsync
        .WithRequest<SubmitRegisterUserRequest>
        .WithActionResult<int>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterUserEndPoint> _logger;
        public RegisterUserEndPoint(UserManager<ApplicationUser> userManager
            , ILogger<RegisterUserEndPoint> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost(SubmitRegisterUserRequest.EndpointUri)]
        public override async Task<ActionResult<int>> HandleAsync
            (SubmitRegisterUserRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = request.Request.Email,
                    Email = request.Request.Email,
                    EmailConfirmed = false
                };
                var result = await _userManager.CreateAsync(user, request.Request.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"User: {user.Email} registered at {DateTime.UtcNow}");
                    return Ok(true);
                }
                return BadRequest(false);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }
    }
}
