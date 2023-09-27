using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Teacher
{
    public class GetAllSubmittedWritingCorrectionSummaryEndpoint : EndpointBaseAsync
        .WithRequest<GetAllSubmittedWritingCorrectionsSummaryRequest>
        .WithResult<GetAllSubmittedWritingCorrectionsSummaryRequest.Response>
    {
        private readonly ICustomRepositoryServices<PaymentGroup, string> _paymentGroupRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserServices _userServices;
        public GetAllSubmittedWritingCorrectionSummaryEndpoint(ICustomRepositoryServices<PaymentGroup, string> paymentGroupRepository,
            ApplicationDbContext dbContext,
            IUserServices userServices)
        {
            _paymentGroupRepository = paymentGroupRepository;
            _dbContext = dbContext;
            _userServices = userServices;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(GetAllSubmittedWritingCorrectionsSummaryRequest.EndpointUri)]
        public override async Task<GetAllSubmittedWritingCorrectionsSummaryRequest.Response> HandleAsync(GetAllSubmittedWritingCorrectionsSummaryRequest request, CancellationToken cancellationToken = default)
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
                    var foundUsersId = _userServices.FindUsers(request.SearchPhrase).Select(x => x.Id);
                    submissions = _dbContext.WritingCorrectionSubmissionGroups
                        .Where(x => foundUsersId.Contains(x.UserId)).ToList();
                }
                if (submissions is not null)
                {
                    var submissionSummary = new List<SubmittedServiceSummarySharedModel>();
                    foreach (var submission in submissions)
                    {
                        var payment = await _paymentGroupRepository.FindByIdAsync(submission.PaymentGroupId);
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
                    submissionSummary.Reverse();
                    return new GetAllSubmittedWritingCorrectionsSummaryRequest.Response(submissionSummary);
                }
                return new GetAllSubmittedWritingCorrectionsSummaryRequest.Response(new List<SubmittedServiceSummarySharedModel>());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
