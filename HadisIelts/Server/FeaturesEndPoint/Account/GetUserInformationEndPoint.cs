using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Account
{
    public class GetUserInformationEndPoint : EndpointBaseAsync
        .WithRequest<GetUserInformationRequest>
        .WithActionResult<UserInformationSharedModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public GetUserInformationEndPoint(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        /// <summary>
        /// gets user information if requesting user and requested user are the same
        /// or user is in roles admin or teacher
        /// </summary>
        /// <param name="request">
        /// UserID: Requesting User ID
        /// RequestedserID: Requested User ID
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// user information if the requesting user is authorized and requested user is found
        /// null user information if the requesting user is authorized but requested user is not found
        /// unAuthorized if the requesting user is unauthorized
        /// </returns>
        [Authorize]
        [HttpPost(GetUserInformationRequest.EndPointUri)]
        public override async Task<ActionResult<UserInformationSharedModel>> HandleAsync(GetUserInformationRequest request, CancellationToken cancellationToken = default)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(request.UserID);
                if (user is not null)
                {
                    bool userIsAuthorized = user.Id == request.RequestedUserID ? true : false; ;
                    if (!userIsAuthorized)
                    {
                        userIsAuthorized = await _userManager.IsInRoleAsync(user, "Admin,Teacher");
                    }
                    if (userIsAuthorized)
                    {
                        var requestedUser = await _userManager.FindByIdAsync(request.RequestedUserID);
                        if (requestedUser is not null)
                        {
                            var response = new UserInformationSharedModel(requestedUser.UserName!, requestedUser.Email!)
                            {
                                FirstName = requestedUser.FirstName,
                                LastName = requestedUser.LastName,
                                Skype = requestedUser.Skype,
                            };
                            return Ok(response);
                        }
                        return Ok(new UserInformationSharedModel(null!, null!));
                    }
                }
                return Unauthorized();
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
