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
        public LoginUserEndPoint(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [HttpPost(AccountLoginRequest.EndPointUri)]
        public override async Task<ActionResult<AccountLoginRequest.Response>> HandleAsync(AccountLoginRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _signInManager.UserManager.FindByEmailAsync(request.LoginRequest.Email!);
                if (user is not null)
                {
                    if (user.LockoutEnd > DateTime.UtcNow)
                    {
                        return Ok(new AccountLoginRequest.Response(
                                LoginSuccess: false
                            ));
                    }
                    var result = await _signInManager.PasswordSignInAsync(
                    userName: request.LoginRequest.Email!,
                    password: request.LoginRequest.Password!,
                    isPersistent: request.LoginRequest.KeepSignedIn,
                    lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        await _signInManager.UserManager.ResetAccessFailedCountAsync(user);
                        return Ok(new AccountLoginRequest.Response(
                                LoginSuccess: true
                            ));
                    }
                }
                return Ok(new AccountLoginRequest.Response(
                    LoginSuccess: false));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
