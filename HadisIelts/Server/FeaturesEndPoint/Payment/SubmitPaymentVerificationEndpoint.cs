using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Payment
{
    public class SubmitPaymentVerificationEndpoint : EndpointBaseAsync
        .WithRequest<SubmitPaymentVerificationRequest>
        .WithActionResult<SubmitPaymentVerificationRequest.Response>
    {
        private readonly ICustomRepositoryServices<PaymentPicture, int> _paymentPictureRepossitory;
        public SubmitPaymentVerificationEndpoint(ICustomRepositoryServices<PaymentPicture, int> paymentPictureRepossitory)
        {
            _paymentPictureRepossitory = paymentPictureRepossitory;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(SubmitPaymentVerificationRequest.EndpointUri)]
        public override async Task<ActionResult<SubmitPaymentVerificationRequest.Response>> HandleAsync(SubmitPaymentVerificationRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var picture = await _paymentPictureRepossitory.FindByIdAsync(request.PictureId);
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
                            Id = picture.Id,
                            IsVerified = picture.IsVerified,
                            IsVerificationPending = false,
                            UploadDateTime = picture.UploadDateTime,
                            Message = picture.Message
                        };
                        return Ok(new SubmitPaymentVerificationRequest.Response(updatedPicture));
                    }
                }
                return Conflict();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
