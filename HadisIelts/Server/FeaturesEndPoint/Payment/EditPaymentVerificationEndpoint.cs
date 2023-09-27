using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Payment
{
    public class EditPaymentVerificationEndpoint : EndpointBaseAsync
        .WithRequest<EditPaymentPictureVerificationRequest>
        .WithActionResult<EditPaymentPictureVerificationRequest.Response>
    {
        private readonly ApplicationDbContext _dbContext;
        public EditPaymentVerificationEndpoint(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(EditPaymentPictureVerificationRequest.EndpointUri)]
        public override async Task<ActionResult<EditPaymentPictureVerificationRequest.Response>> HandleAsync(EditPaymentPictureVerificationRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var payment = await _dbContext.PaymentPictures.FindAsync(request.PictureId);
                if (payment is not null)
                {
                    var paymentGroup = await _dbContext.PaymentGroups.FindAsync(payment.PaymentGroupId);
                    if (paymentGroup is not null && paymentGroup.IsPaymentCheckPending)
                    {
                        payment.IsVerificationPending = true;
                        payment.Message = "Verification pending";
                        _dbContext.PaymentPictures.Update(payment);
                        var changes = _dbContext.SaveChanges();
                        return Ok(new EditPaymentPictureVerificationRequest.Response(changes > 0));
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
