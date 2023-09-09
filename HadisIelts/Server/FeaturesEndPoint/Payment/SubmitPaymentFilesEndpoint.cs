using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Payment
{
    public class SubmitPaymentFilesEndpoint : EndpointBaseAsync
        .WithRequest<UploadPaymentPackageRequest>
        .WithActionResult<UploadPaymentPackageRequest.Response>
    {
        private readonly ICustomRepositoryServices<PaymentGroup, string>
            _paymentGroupRepository;
        private readonly ICustomRepositoryServices<PaymentPicture, int>
            _paymentPictureRepository;
        public SubmitPaymentFilesEndpoint(
            ICustomRepositoryServices<PaymentGroup, string> paymentGroupRepository
            , ICustomRepositoryServices<PaymentPicture, int> paymentPictureRepository)
        {
            _paymentGroupRepository = paymentGroupRepository;
            _paymentPictureRepository = paymentPictureRepository;
        }
        /// <summary>
        /// finds the payment group created on submitting writing correction files
        /// if payment group is found, payment pictures get uploaded and payment group statues will change
        /// </summary>
        /// <param name="request">
        /// PaymentPictures: Payment Pictures uploaded by user 
        /// PaymentID: Payment Group ID created on submitting writig correction files 
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// Submitted Files: Payment pictures uploaded to db
        /// Message: verification pending message
        /// </returns>
        [Authorize]
        [HttpPost(UploadPaymentPackageRequest.EndpointUri)]
        public override async Task<ActionResult<UploadPaymentPackageRequest.Response>> HandleAsync(UploadPaymentPackageRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var paymentGroup = await _paymentGroupRepository.FindByIDAsync(request.PaymentID);
                if (paymentGroup is not null)
                {
                    var paymentFiles = new List<PaymentPicture>();
                    foreach (var payment in request.PaymentPictures)
                    {
                        var paymentFile = new PaymentPicture
                        {
                            Name = payment.Name,
                            Data = payment.Data,
                            FileSuffix = payment.FileSuffix,
                            PaymentGroupID = request.PaymentID,
                            IsVerified = false,
                            Message = "Pending to verify",
                            UploadDateTime = DateTime.UtcNow,
                        };
                        var addedPayment = _paymentPictureRepository.Insert(paymentFile);
                        paymentFile.ID = addedPayment.ID;
                        paymentFiles.Add(paymentFile);
                    }
                    paymentGroup.Message = "Thank you for your payment, please wait while we verify your payment";
                    paymentGroup.IsPaymentCheckPending = true;
                    var paymentGroupIsUpdated = _paymentGroupRepository.Update(paymentGroup);
                    if (paymentGroupIsUpdated)
                    {
                        var submittedPaymentfiles = new List<PaymentPictureSharedModel>();
                        foreach (var payment in paymentFiles)
                        {
                            submittedPaymentfiles.Add(new PaymentPictureSharedModel
                            {
                                Data = payment.Data,
                                FileSuffix = payment.FileSuffix,
                                IsVerified = false,
                                Message = payment.Message,
                                Name = payment.Name,
                                UploadDateTime = payment.UploadDateTime,
                            });
                        }
                        return Ok(new UploadPaymentPackageRequest.Response(
                            PaymentPictures: submittedPaymentfiles,
                            Message: paymentGroup.Message
                        ));
                    }
                    return Problem("Payment group was not updated");
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
