using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.Telegram;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HadisIelts.Server.FeaturesEndPoint.Payment
{
    public class SubmitPaymentFilesEndpoint : EndpointBaseAsync
        .WithRequest<UploadPaymentPackageRequest>
        .WithActionResult<UploadPaymentPackageRequest.Response>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ITelegramServices _telegramServices;
        private readonly UserManager<ApplicationUser> _userManager;
        public SubmitPaymentFilesEndpoint(ApplicationDbContext dbContext,
            ITelegramServices telegramServices,
            UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _telegramServices = telegramServices;
            _userManager = userManager;
        }
        /// <summary>
        /// finds the payment group created on submitting writing correction files
        /// if payment group is found, payment pictures get uploaded and payment group statues will change
        /// </summary>
        /// <param name="request">
        /// PaymentPictures: Payment Pictures uploaded by user 
        /// PaymentId: Payment Group Id created on submitting writig correction files 
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
                var paymentGroup = await _dbContext.PaymentGroups.FindAsync(request.PaymentId);
                if (paymentGroup is not null)
                {
                    var paymentFiles = await _dbContext.PaymentPictures.Where(x => x.PaymentGroupId == paymentGroup.Id).ToListAsync();
                    foreach (var payment in request.PaymentPictures)
                    {
                        var paymentFile = new PaymentPicture
                        {
                            Name = payment.Name,
                            Data = Convert.FromBase64String(payment.Data),
                            FileSuffix = payment.FileSuffix,
                            PaymentGroupId = request.PaymentId,
                            IsVerified = false,
                            IsVerificationPending = true,
                            Message = "Verification pending",
                            UploadDateTime = DateTime.UtcNow,
                        };
                        var addedPayment = _dbContext.PaymentPictures.Add(paymentFile);
                        paymentFile.Id = addedPayment.Entity.Id;
                        paymentFiles.Add(paymentFile);
                    }
                    paymentGroup.Message = "Verification Pending";
                    paymentGroup.IsPaymentCheckPending = true;
                    paymentGroup.LastUpdateDateTime = DateTime.UtcNow;
                    _dbContext.PaymentGroups.Update(paymentGroup);
                    var changes = _dbContext.SaveChanges();
                    if (changes > 0)
                    {
                        var submittedPaymentfiles = new List<PaymentPictureSharedModel>();
                        foreach (var payment in paymentFiles)
                        {
                            submittedPaymentfiles.Add(new PaymentPictureSharedModel
                            {
                                Id = payment.Id,
                                Data = Convert.ToBase64String(payment.Data),
                                FileSuffix = payment.FileSuffix,
                                IsVerified = false,
                                Message = payment.Message,
                                Name = payment.Name,
                                UploadDateTime = payment.UploadDateTime,
                                IsVerificationPending = payment.IsVerificationPending,
                            });
                        }
                        var user = await _userManager.FindByIdAsync(paymentGroup.UserId);
                        var submissionGroup = await _dbContext.WritingCorrectionSubmissionGroups.FindAsync(paymentGroup.SubmittedServiceId);
                        if (user is not null && submissionGroup is not null)
                        {
                            await _telegramServices.SendMessage
                            ($"{user.FirstName} {user.LastName} with email address: {user.Email} " +
                            $"submitted a payment for writing correction price:{submissionGroup.TotalPrice / 10000} thousand Tomans");
                        }
                        return Ok(new UploadPaymentPackageRequest.Response(
                        PaymentPictures: submittedPaymentfiles,
                            Message: paymentGroup.Message
                        ));
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
