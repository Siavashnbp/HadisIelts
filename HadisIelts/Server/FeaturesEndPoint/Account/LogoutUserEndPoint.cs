using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Shared.Requests.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Account
{
    public class LogoutUserEndPoint : EndpointBaseAsync
        .WithoutRequest
        .WithoutResult
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        public LogoutUserEndPoint(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [Authorize]
        [HttpPost(AccountLogoutRequest.EndPointUri)]
        public override async Task HandleAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
