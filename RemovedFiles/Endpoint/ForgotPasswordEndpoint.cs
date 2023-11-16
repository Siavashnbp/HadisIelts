using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Server.Services.Email;
using HadisIelts.Shared.Requests.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Account
{
    public class ForgotPasswordEndpoint : EndpointBaseAsync
        .WithRequest<ForgotPasswordRequest>.
        WithoutResult
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailServices _emailServices;
        private readonly NavigationManager _navigationManager;
        public ForgotPasswordEndpoint(UserManager<ApplicationUser> userManager,
            IEmailServices emailServices,
            NavigationManager navigationManager)
        {
            _userManager = userManager;
            _emailServices = emailServices;
            _navigationManager = navigationManager;
        }
        [HttpPost(ForgotPasswordRequest.EndpointUri)]
        public override async Task HandleAsync(ForgotPasswordRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var message = new EmailMessage(user.Email!);
                    message.Subject = "Reset Password";
                    var parameters = new Dictionary<string, object?>
                    {
                        { "userId", user.Id },
                        { "token", token }
                    };
                    var address = _navigationManager.GetUriWithQueryParameters("https://www.englishwithhadis.com/account/resetPassword",
                        parameters);
                    message.Content = $"please click on the link to reset your password.<a href=\"{address}\">link</a>";
                    _emailServices.SendEmail(message);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
