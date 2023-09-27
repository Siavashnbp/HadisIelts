using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Payment
{
    public class EditPaymentVerificationEndpoint : EndpointBaseAsync
        .WithRequest<EditPaymentPictureVerificationRequest>
        .WithActionResult<EditPaymentPictureVerificationRequest.Response>
    {
        private readonly ICustomRepositoryServices<PaymentPicture, int> _paymentPictureRepository;
        private readonly ICustomRepositoryServices<PaymentGroup, string> _paymentGroupRepository;
        public EditPaymentVerificationEndpoint(ICustomRepositoryServices<PaymentPicture, int> paymentPictureRepository,
            ICustomRepositoryServices<PaymentGroup, string> paymentGroupRepository)
        {
            _paymentPictureRepository = paymentPictureRepository;
            _paymentGroupRepository = paymentGroupRepository;

        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(EditPaymentPictureVerificationRequest.EndpointUri)]
        public override async Task<ActionResult<EditPaymentPictureVerificationRequest.Response>> HandleAsync(EditPaymentPictureVerificationRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var payment = await _paymentPictureRepository.FindByIdAsync(request.PictureId);
                if (payment is not null)
                {
                    var paymentGroup = await _paymentGroupRepository.FindByIdAsync(payment.PaymentGroupId);
                    if (paymentGroup is not null && paymentGroup.IsPaymentCheckPending)
                    {
                        payment.IsVerificationPending = true;
                        payment.Message = "Verification pending";
                        var wasSuccessful = _paymentPictureRepository.Update(payment);
                        return Ok(new EditPaymentPictureVerificationRequest.Response(wasSuccessful));
                    }
                    return Problem("Payment group is already checked");
                }
                return Problem("Payment picture was not found");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
