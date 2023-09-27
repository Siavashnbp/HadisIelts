using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Requests.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Account
{
    public class UpdateAccountInformationEndpoint : EndpointBaseAsync
        .WithRequest<UpdateAccountInformationRequest>
        .WithActionResult<UpdateAccountInformationRequest.Response>
    {
        private readonly ICustomRepositoryServices<ApplicationUser, string> _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public UpdateAccountInformationEndpoint(ICustomRepositoryServices<ApplicationUser, string> userRepository,
            UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        [Authorize]
        [HttpPost(UpdateAccountInformationRequest.EndpointUri)]
        public override async Task<ActionResult<UpdateAccountInformationRequest.Response>> HandleAsync(UpdateAccountInformationRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.UserInformation.Email);
                if (user is not null)
                {
                    user.FirstName = request.UserInformation.FirstName!;
                    user.LastName = request.UserInformation.LastName!;
                    user.Skype = request.UserInformation.Skype;
                    user.DateOfBirth = Convert.ToDateTime(request.UserInformation.Birthday);
                    var wasSuccessful = _userRepository.Update(user);
                    if (wasSuccessful)
                    {
                        return Ok(new UpdateAccountInformationRequest.Response(request.UserInformation));
                    }
                    return Problem("user cannot be updated");
                }
                return Problem("user was not found");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
