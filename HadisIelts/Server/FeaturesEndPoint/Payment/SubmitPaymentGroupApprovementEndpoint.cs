using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models;
using HadisIelts.Server.Services.Email;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Payment
{
    public class SubmitPaymentGroupApprovementEndpoint : EndpointBaseAsync
        .WithRequest<SubmitPaymentGroupApprovementRequest>
        .WithActionResult<SubmitPaymentGroupApprovementRequest.Response>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailServices _emailServices;
        private readonly UserManager<ApplicationUser> _userManager;
        public SubmitPaymentGroupApprovementEndpoint(ApplicationDbContext dbContext,
            IEmailServices emailServices,
            UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _emailServices = emailServices;
            _userManager = userManager;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(SubmitPaymentGroupApprovementRequest.EndpointUri)]
        public override async Task<ActionResult<SubmitPaymentGroupApprovementRequest.Response>> HandleAsync(SubmitPaymentGroupApprovementRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var paymentGroup = await _dbContext.PaymentGroups.FindAsync(request.PaymentGroupId);
                if (paymentGroup != null)
                {
                    if (!paymentGroup.IsPaymentCheckPending)
                    {
                        return Ok(new SubmitPaymentGroupApprovementRequest.Response(WasSuccessful: false,
                            Message: "Payment is already checked"));
                    }
                    var paymentPictures = _dbContext.PaymentPictures.Where(x => x.PaymentGroupId == paymentGroup.Id).ToList();
                    paymentPictures.Select(x =>
                    {
                        if (x.IsVerificationPending)
                        {
                            x.IsVerified = request.IsApproved;
                            x.IsVerificationPending = false;
                            x.Message = request.IsApproved ? "Verified" : "Rejected";
                        }
                        return x;
                    });

                    _dbContext.PaymentPictures.UpdateRange(paymentPictures);
                    paymentGroup.IsPaymentApproved = request.IsApproved;
                    paymentGroup.IsPaymentCheckPending = false;
                    paymentGroup.Message = request.IsApproved ? "Payment is approved" : "Payment is rejected";
                    paymentGroup.LastUpdateDateTime = DateTime.UtcNow;
                    _dbContext.PaymentGroups.Update(paymentGroup);
                    var changes = _dbContext.SaveChanges();
                    if (changes > 0)
                    {
                        var user = await _userManager.FindByIdAsync(paymentGroup.UserId);
                        if (user != null)
                        {
                            var emailMessage = new EmailMessage(user.Email!);
                            emailMessage.Subject = "Writing Correction Payment";
                            emailMessage.Content = $"Your payment has benn checked and " +
                                $"{(paymentGroup.IsPaymentApproved ? "it is approved. Your writings will be corrected soon."
                                : "it is rejectd. Please check your submitted payment files.")} ";
                            _emailServices.SendEmail(emailMessage);
                        }
                        return Ok(new SubmitPaymentGroupApprovementRequest.Response(WasSuccessful: changes > 0,
                            Message: paymentGroup.Message));
                    }
                    return Conflict();
                }

                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
