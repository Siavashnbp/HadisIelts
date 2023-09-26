using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Requests.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Account
{
    public class UpdateAccountInformationEndpoint : EndpointBaseAsync
        .WithRequest<UpdateAccountInformationRequest>
        .WithActionResult<UpdateAccountInformationRequest.Response>
    {
        private readonly ICustomRepositoryServices<ApplicationUser, string> _userRepository;
        [Authorize]
        [HttpPost(UpdateAccountInformationRequest.EndpointUri)]
        public override Task<ActionResult<UpdateAccountInformationRequest.Response>> HandleAsync(UpdateAccountInformationRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
