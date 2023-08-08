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
            try
            {
                var user = await _signInManager.UserManager.FindByEmailAsync(request.Request.Email);
                if (user is not null)
                {
                    if (user.LockoutEnd > DateTime.UtcNow)
                    {
                        return Ok(new AccountLoginRequest.Response
                            (new LoginResponse
                            {
                                LoginSucess = false,
                                Message = $"Too many failed attempts. Try in {(user.LockoutEnd - DateTime.UtcNow).Value.Minutes + 1} minutes"
                            }));
                    }
                    var result = await _signInManager.PasswordSignInAsync(
                    userName: request.Request.Email,
                    password: request.Request.Password,
                    isPersistent: request.Request.KeepSignedIn,
                    lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        await _signInManager.UserManager.ResetAccessFailedCountAsync(user);
                        return Ok(new AccountLoginRequest.Response(
                            new LoginResponse
                            {
                                LoginSucess = true,
                                Message = string.Empty
                            }));
                    }
                }
                return Ok(new AccountLoginRequest.Response(
                    new LoginResponse
                    {
                        LoginSucess = false,
                        Message = "Username or password is incorrect"
                    }));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
