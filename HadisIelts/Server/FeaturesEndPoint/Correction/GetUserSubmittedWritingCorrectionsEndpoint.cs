using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Correction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HadisIelts.Server.FeaturesEndPoint.Correction
{
    public class GetUserSubmittedWritingCorrectionsEndpoint : EndpointBaseAsync
        .WithRequest<GetUserSubmittedWritingCorrectionRequest>
        .WithActionResult<GetUserSubmittedWritingCorrectionRequest.Response>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserServices _userServices;
        public GetUserSubmittedWritingCorrectionsEndpoint(ApplicationDbContext dbContext,
            IUserServices userServices)
        {
            _dbContext = dbContext;
            _userServices = userServices;
        }
        [Authorize]
        [HttpPost(GetUserSubmittedWritingCorrectionRequest.EndpointUri)]
        public override async Task<ActionResult<GetUserSubmittedWritingCorrectionRequest.Response>> HandleAsync(GetUserSubmittedWritingCorrectionRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (_userServices.IsUserOwnerOrSpecificRoles
                    (claims: User.Claims.ToList(), roles: new List<string> { "Administrator", "Teacher" }, userId: request.UserId))
                {
                    var submissions = _dbContext.WritingCorrectionSubmissionGroups.Where(x => x.UserId == request.UserId).ToList();
                    if (submissions is not null)
                    {
                        var submissionSummary = new List<SubmittedServiceSummarySharedModel>();
                        foreach (var submission in submissions)
                        {
                            var payment = await _dbContext.PaymentGroups.FindAsync(submission.PaymentGroupId);
                            submissionSummary.Add(new SubmittedServiceSummarySharedModel
                            {
                                PaymentId = submission.PaymentGroupId,
                                PaymentStatus = payment is null ? "payment not submitted" : payment.Message,
                                SubmissionDateTime = submission.SubmissionDateTime,
                                SubmittedServiceId = submission.Id,
                                IsCorrected = submission.IsCorrected,
                            });
                        }
                        var sortedSubmissions = submissionSummary.OrderByDescending(x => x.SubmissionDateTime).ToList();
                        return Ok(new GetUserSubmittedWritingCorrectionRequest.Response(sortedSubmissions, HttpStatusCode.OK));
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
