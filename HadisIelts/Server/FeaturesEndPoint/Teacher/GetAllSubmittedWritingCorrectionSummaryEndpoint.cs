using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HadisIelts.Server.FeaturesEndPoint.Teacher
{
    public class GetAllSubmittedWritingCorrectionSummaryEndpoint : EndpointBaseAsync
        .WithRequest<GetAllSubmittedWritingCorrectionsSummaryRequest>
        .WithActionResult<GetAllSubmittedWritingCorrectionsSummaryRequest.Response>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserServices _userServices;
        public GetAllSubmittedWritingCorrectionSummaryEndpoint(ApplicationDbContext dbContext,
            IUserServices userServices)
        {
            _dbContext = dbContext;
            _userServices = userServices;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(GetAllSubmittedWritingCorrectionsSummaryRequest.EndpointUri)]
        public override async Task<ActionResult<GetAllSubmittedWritingCorrectionsSummaryRequest.Response>> HandleAsync(GetAllSubmittedWritingCorrectionsSummaryRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var submissions = new List<WritingCorrectionSubmissionGroup>();
                if (request.SearchPhrase is null || request.SearchPhrase == string.Empty)
                {
                    submissions = _dbContext.WritingCorrectionSubmissionGroups.ToList();
                }
                else
                {
                    var foundUsersId = (await _userServices.FindUsers(request.SearchPhrase)).Select(x => x.Id);
                    submissions = await _dbContext.WritingCorrectionSubmissionGroups
                        .Where(x => foundUsersId.Contains(x.UserId)).ToListAsync();
                }
                if (submissions is not null)
                {
                    var submissionSummary = new List<SubmittedServiceSummarySharedModel>();
                    foreach (var submission in submissions)
                    {
                        var payment = await _dbContext.PaymentGroups.FindAsync(submission.PaymentGroupId);
                        submissionSummary.Add(new SubmittedServiceSummarySharedModel
                        {
                            UserDetails = await _userServices.GetUserInformationAsync(submission.UserId),
                            PaymentId = submission.PaymentGroupId,
                            PaymentStatus = payment is null ? "payment not submitted" : payment.Message,
                            SubmissionDateTime = submission.SubmissionDateTime,
                            SubmittedServiceId = submission.Id,
                            IsCorrected = submission.IsCorrected,
                        });
                    }
                    var sortedSubmissions = submissionSummary.OrderBy(x => x.IsCorrected).ThenByDescending(x => x.SubmissionDateTime).ToList();
                    return Ok(new GetAllSubmittedWritingCorrectionsSummaryRequest.Response(sortedSubmissions));
                }
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
