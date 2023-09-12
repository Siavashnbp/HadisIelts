using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Correction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Correction
{
    public class GetUserSubmittedWritingCorrectionsEndpoint : EndpointBaseAsync
        .WithRequest<GetUserSubmittedWritingCorrectionRequest>
        .WithResult<GetUserSubmittedWritingCorrectionRequest.Response>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICustomRepositoryServices<PaymentGroup, string> _paymentGroupRepository;
        private readonly IUserServices _userServices;
        public GetUserSubmittedWritingCorrectionsEndpoint(ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            ICustomRepositoryServices<PaymentGroup, string> paymentGroupRepository,
            IUserServices userServices)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _paymentGroupRepository = paymentGroupRepository;
            _userServices = userServices;
        }
        [Authorize]
        [HttpPost(GetUserSubmittedWritingCorrectionRequest.EndpointUri)]
        public override async Task<GetUserSubmittedWritingCorrectionRequest.Response> HandleAsync(GetUserSubmittedWritingCorrectionRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserID);
                if (user is not null)
                {
                    if (_userServices.IsUserOwnerOrSpecificRoles
                        (claims: User.Claims.ToList(), roles: new List<string> { "Administrator", "Teacher" }, userID: user.Id))
                    {
                        var submissions = _dbContext.SubmittedWritingCorrectionFiles.Where(x => x.UserID == user.Id).ToList();
                        if (submissions is not null)
                        {
                            var submissionSummary = new List<SubmittedServiceSummarySharedModel>();
                            foreach (var submission in submissions)
                            {
                                var payment = await _paymentGroupRepository.FindByIDAsync(submission.PaymentGroupID);
                                submissionSummary.Add(new SubmittedServiceSummarySharedModel
                                {
                                    PaymentID = submission.PaymentGroupID,
                                    PaymentStatus = payment is null ? "payment not submitted" : payment.Message,
                                    SubmissionDateTime = submission.SubmissionDateTime,
                                    SubmittedServiceID = submission.ID
                                });
                            }
                            submissionSummary.Reverse();
                            return new GetUserSubmittedWritingCorrectionRequest.Response(submissionSummary);
                        }
                    }
                }
                return new GetUserSubmittedWritingCorrectionRequest.Response(new List<SubmittedServiceSummarySharedModel>());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
