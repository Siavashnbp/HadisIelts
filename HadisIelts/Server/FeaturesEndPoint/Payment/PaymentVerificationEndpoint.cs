using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Payment
{
    public class PaymentVerificationEndpoint : EndpointBaseAsync
        .WithRequest<PaymentVerificationRequest>
        .WithActionResult<PaymentVerificationRequest.Response>
    {
        private readonly ICustomRepositoryServices<PaymentPicture, int> _paymentPictureRepossitory;
        public PaymentVerificationEndpoint(ICustomRepositoryServices<PaymentPicture, int> paymentPictureRepossitory)
        {
            _paymentPictureRepossitory = paymentPictureRepossitory;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(PaymentVerificationRequest.EndpointUri)]
        public override async Task<ActionResult<PaymentVerificationRequest.Response>> HandleAsync(PaymentVerificationRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var picture = await _paymentPictureRepossitory.FindByIDAsync(request.PaymentID);
                if (picture != null)
                {
                    picture.IsVerified = request.IsVerfifed;
                    picture.IsVerificationPending = false;
                    picture.Message = picture.IsVerified ? "Verified" : "Rejected";
                    var wasUpdateSuccessful = _paymentPictureRepossitory.Update(picture);
                    if (wasUpdateSuccessful)
                    {
                        var updatedPicture = new PaymentPictureSharedModel
                        {
                            Name = picture.Name,
                            Data = picture.Data,
                            FileSuffix = picture.FileSuffix,
                            ID = picture.ID,
                            IsVerified = picture.IsVerified,
                            IsVerificationPending = false,
                            UploadDateTime = picture.UploadDateTime,
                            Message = picture.Message
                        };
                        return Ok(new PaymentVerificationRequest.Response(updatedPicture));
                    }
                    return Problem("Picture could not be updated");
                }
                return Problem("Picture was not found");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
