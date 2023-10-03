using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HadisIelts.Server.FeaturesEndPoint.Account
{
    public class GetUserInformationEndPoint : EndpointBaseAsync
        .WithRequest<GetUserInformationRequest>
        .WithActionResult<GetUserInformationRequest.Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserServices _userServices;
        public GetUserInformationEndPoint(UserManager<ApplicationUser> userManager,
            IUserServices userServices)
        {
            _userManager = userManager;
            _userServices = userServices;
        }
        /// <summary>
        /// gets user information if requesting user and requested user are the same
        /// or user is in roles admin or teacher
        /// </summary>
        /// <param name="request">
        /// UserId: Requesting User Id
        /// RequestedserId: Requested User Id
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// user information if the requesting user is authorized and requested user is found
        /// null user information if the requesting user is authorized but requested user is not found
        /// unAuthorized if the requesting user is unauthorized
        /// </returns>
        [Authorize]
        [HttpPost(GetUserInformationRequest.EndPointUri)]
        public override async Task<ActionResult<GetUserInformationRequest.Response>> HandleAsync(GetUserInformationRequest request, CancellationToken cancellationToken = default)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user is not null)
                {
                    var isUserAuthorized = _userServices.IsUserOwnerOrSpecificRoles
                        (User.Claims.ToList(), new List<string> { "Administrator", "Teacher" }, request.UserId);
                    if (isUserAuthorized)
                    {
                        var requestedUser = await _userManager.FindByIdAsync(request.UserId);
                        if (requestedUser is not null)
                        {
                            var response = new UserInformationSharedModel(requestedUser.UserName!, requestedUser.Email!)
                            {
                                FirstName = requestedUser.FirstName,
                                LastName = requestedUser.LastName,
                                Skype = requestedUser.Skype,
                            };
                            return Ok(new GetUserInformationRequest.Response(response, HttpStatusCode.OK));
                        }
                        return NoContent();
                    }
                }
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
