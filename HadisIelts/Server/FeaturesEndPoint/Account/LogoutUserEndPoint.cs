using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Shared.Requests.Account;
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
        [HttpPost(AccountLogoutRequest.EndPointUri)]
        public override async Task HandleAsync(CancellationToken cancellationToken = default)
        {
            await _signInManager.SignOutAsync();
        }
    }
}
