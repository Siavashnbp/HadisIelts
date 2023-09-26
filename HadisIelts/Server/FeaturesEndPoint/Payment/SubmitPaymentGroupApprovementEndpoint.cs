using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Payment
{
    public class SubmitPaymentGroupApprovementEndpoint : EndpointBaseAsync
        .WithRequest<SubmitPaymentGroupApprovementRequest>
        .WithActionResult<SubmitPaymentGroupApprovementRequest.Response>
    {
        private readonly ICustomRepositoryServices<PaymentGroup, string> _paymentGroupRepository;
        private readonly ICustomRepositoryServices<PaymentPicture, int> _paymentPictureRepository;
        private readonly ApplicationDbContext _dbContext;
        public SubmitPaymentGroupApprovementEndpoint(ICustomRepositoryServices<PaymentGroup, string> paymentGroupRepository,
            ApplicationDbContext dbContext,
            ICustomRepositoryServices<PaymentPicture, int> paymentPictureRepository)
        {
            _paymentGroupRepository = paymentGroupRepository;
            _dbContext = dbContext;
            _paymentPictureRepository = paymentPictureRepository;

        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(SubmitPaymentGroupApprovementRequest.EndpointUri)]
        public override async Task<ActionResult<SubmitPaymentGroupApprovementRequest.Response>> HandleAsync(SubmitPaymentGroupApprovementRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var paymentGroup = await _paymentGroupRepository.FindByIdAsync(request.PaymentGroupId);
                if (paymentGroup != null)
                {
                    if (!paymentGroup.IsPaymentCheckPending)
                    {
                        return BadRequest(new SubmitPaymentGroupApprovementRequest.Response(WasSuccessful: false,
                            Message: "Payment group is already checked"));
                    }
                    var paymentPictures = _dbContext.PaymentPictures.Where(x => x.PaymentGroupId == paymentGroup.Id).ToList();
                    if (paymentPictures.Any(x => !x.IsVerified && !x.IsVerificationPending))
                    {
                        return Ok(new SubmitPaymentGroupApprovementRequest.Response(WasSuccessful: false,
                            Message: "One of payments is rejected"));
                    }
                    paymentPictures.Select(x => { x.IsVerified = request.IsApproved; x.IsVerificationPending = false; return x; });
                    var wasPicturesUpdateSuccessful = _paymentPictureRepository.UpdateAll(paymentPictures);
                    if (wasPicturesUpdateSuccessful)
                    {
                        paymentGroup.IsPaymentApproved = request.IsApproved;
                        paymentGroup.IsPaymentCheckPending = false;
                        paymentGroup.Message = request.IsApproved ? "Payment group is approved" : "Payment group is rejected";
                        paymentGroup.LastUpdateDateTime = DateTime.UtcNow;
                        var wasPaymentGroupUpdatSuccessful = _paymentGroupRepository.Update(paymentGroup);
                        return Ok(new SubmitPaymentGroupApprovementRequest.Response(WasSuccessful: wasPaymentGroupUpdatSuccessful,
                            Message: paymentGroup.Message));
                    }
                    return Problem("Payment pictures could not be updated");

                }
                return Problem("Payment group was not found");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
