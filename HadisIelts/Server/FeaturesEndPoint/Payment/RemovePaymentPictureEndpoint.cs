using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Payment
{
    public class RemovePaymentPictureEndpoint : EndpointBaseAsync
        .WithRequest<RemovePaymentPictureRequest>
        .WithActionResult<RemovePaymentPictureRequest.Response>
    {
        private readonly ICustomRepositoryServices<PaymentPicture, int> _paymentPictureRepository;
        private readonly ICustomRepositoryServices<PaymentGroup, string> _paymentGroupRepository;
        private readonly IUserServices _userServices;
        public RemovePaymentPictureEndpoint(ICustomRepositoryServices<PaymentPicture, int> paymentPictureRepository,
            IUserServices userServices,
            ICustomRepositoryServices<PaymentGroup, string> paymentGroupRepository)
        {
            _paymentPictureRepository = paymentPictureRepository;
            _userServices = userServices;
            _paymentGroupRepository = paymentGroupRepository;
        }
        [Authorize]
        [HttpPost(RemovePaymentPictureRequest.EndpointUri)]
        public override async Task<ActionResult<RemovePaymentPictureRequest.Response>> HandleAsync(RemovePaymentPictureRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var payment = await _paymentPictureRepository.FindByIdAsync(request.PaymentId);
                if (payment != null && !payment.IsVerified)
                {
                    var paymentGroup = await _paymentGroupRepository.FindByIdAsync(payment.PaymentGroupId);
                    if (paymentGroup != null)
                    {
                        if (_userServices.IsUserOwnerOrSpecificRoles(
                            claims: User.Claims.ToList(), new List<string> { "Administrator", "Teacher" }, paymentGroup.UserId))
                        {
                            var wasRemoveSuccessful = _paymentPictureRepository.Delete(payment);
                            if (wasRemoveSuccessful)
                            {
                                return Ok(new RemovePaymentPictureRequest.Response(true));
                            }
                        }
                        return Unauthorized();
                    }
                    return Problem("Payment could not be removed");
                }
                return Problem("Payment was not found");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
