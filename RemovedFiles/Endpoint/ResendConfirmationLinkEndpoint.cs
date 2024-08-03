using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Server.Services.Email;
using HadisIelts.Shared.Requests.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Account
{
    public class ResendConfirmationLinkEndpoint : EndpointBaseAsync
        .WithRequest<ResendConfirmationLinkRequest>
        .WithActionResult
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailServices _emailServices;
        private readonly NavigationManager _navigationManager;
        public ResendConfirmationLinkEndpoint(UserManager<ApplicationUser> userManager,
            IEmailServices emailServices, NavigationManager navigationManager)
        {
            _userManager = userManager;
            _emailServices = emailServices;
            _navigationManager = navigationManager;
        }
        [HttpPost(ResendConfirmationLinkRequest.EndpointUri)]
        public override async Task<ActionResult> HandleAsync(ResendConfirmationLinkRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var parameters = new Dictionary<string, object?>
                        {
                            { "userId",user.Id },
                            { "token",token }
                        };
                    var link = _navigationManager.GetUriWithQueryParameters
                        ("http://englishwithhadis.com/account/emailConfirmation", parameters);
                    var message = new EmailMessage(user.Email!);
                    message.Subject = "Email Confirmation";
                    message.Content = $"Please click on the link to confirm your email. <a href=\"{link}\">link</a>";
                    _emailServices.SendEmail(message);
                    return Ok();
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
