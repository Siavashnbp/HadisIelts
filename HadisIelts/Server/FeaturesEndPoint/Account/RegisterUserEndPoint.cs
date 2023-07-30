using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Shared.Requests.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Account
{
    public class RegisterUserEndPoint : EndpointBaseAsync
        .WithRequest<RegisterAccountRequest>
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

        [HttpPost(RegisterAccountRequest.EndpointUri)]
        public override async Task<ActionResult<int>> HandleAsync
            (RegisterAccountRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = new ApplicationUser
                {
                    FirstName = "test",
                    LastName = "test",
                    UserName = request.Request.Email,
                    Email = request.Request.Email,
                    EmailConfirmed = true
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
