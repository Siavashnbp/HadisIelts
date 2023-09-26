using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Requests.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.User
{
    public class UploadWritingCorrectionAllowanceEndpoint : EndpointBaseSync
        .WithoutRequest
        .WithActionResult<UploadWritingCorrectionAllowanceRequest.Response>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserServices _userServices;
        public UploadWritingCorrectionAllowanceEndpoint(ApplicationDbContext dbContext,
            IUserServices userServices)
        {
            _dbContext = dbContext;
            _userServices = userServices;
        }
        [Authorize]
        [HttpGet(UploadWritingCorrectionAllowanceRequest.EndpointUri)]

        public override ActionResult<UploadWritingCorrectionAllowanceRequest.Response> Handle()
        {
            try
            {
                var userId = _userServices.GetUserIdFromClaims(User.Claims.ToList());
                if (userId is not null)
                {
                    var HasCorrectionPending = _userServices.HasWritingCorrectionPending(_dbContext, userId);
                    return Ok(new UploadWritingCorrectionAllowanceRequest.Response(IsAllowed: !HasCorrectionPending));
                }
                return Problem("User was not found");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
