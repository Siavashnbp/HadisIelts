using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Shared.Requests.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Account
{
    public class GetUserInformationEndPoint : EndpointBaseAsync
        .WithRequest<GetUserInformationRequest>
        .WithResult<UserInformation>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public GetUserInformationEndPoint(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [Authorize]
        [HttpPost(GetUserInformationRequest.EndPointUri)]
        public override async Task<UserInformation> HandleAsync(GetUserInformationRequest request, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByIdAsync(request.userID);
            if (user is not null)
            {
                var response = new UserInformation
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Skype = user.Skype,
                    Username = user.UserName
                };
                return response;
            }
            return new UserInformation();
        }
    }
}
