using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Shared.Requests;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HadisIelts.Server.FeaturesEndPoint.Payment
{
    public class SubmitPaymentGroupApprovementEndpoint : EndpointBaseAsync
        .WithRequest<SubmitPaymentGroupApprovementRequest>
        .WithActionResult<SubmitPaymentGroupApprovementRequest.Response>
    {
        private readonly ApplicationDbContext _dbContext;
        public SubmitPaymentGroupApprovementEndpoint(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
                        return BadRequest(new SubmitPaymentGroupApprovementRequest.Response(WasSuccessful: false));
                    }
                    var paymentPictures = _dbContext.PaymentPictures.Where(x => x.PaymentGroupId == paymentGroup.Id).ToList();
                    if (paymentPictures.Any(x => !x.IsVerified && !x.IsVerificationPending))
                    {
                        return Ok(new SubmitPaymentGroupApprovementRequest.Response(WasSuccessful: false));
                    }
                    paymentPictures.Select(x => { x.IsVerified = request.IsApproved; x.IsVerificationPending = false; return x; });
                    _dbContext.PaymentPictures.UpdateRange(paymentPictures);
                    paymentGroup.IsPaymentApproved = request.IsApproved;
                    paymentGroup.IsPaymentCheckPending = false;
                    paymentGroup.Message = request.IsApproved ? "Payment group is approved" : "Payment group is rejected";
                    paymentGroup.LastUpdateDateTime = DateTime.UtcNow;
                    _dbContext.PaymentGroups.Update(paymentGroup);
                    var changes = _dbContext.SaveChanges();
                    return Ok(new SubmitPaymentGroupApprovementRequest.Response(WasSuccessful: changes > 0));
                }
                return NotFound(new ServerResponse(HttpStatusCode.NotFound, "Payment group was not found"));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
