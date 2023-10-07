using Ardalis.ApiEndpoints;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Payment
{
    public class EditPaymentGroupApprovementEndpoint : EndpointBaseAsync
        .WithRequest<EditPaymentGroupApprovementRequest>
        .WithActionResult<EditPaymentGroupApprovementRequest.Respone>
    {
        private readonly ICustomRepositoryServices<PaymentGroup, string> _paymentGroupRepository;
        public EditPaymentGroupApprovementEndpoint(ICustomRepositoryServices<PaymentGroup, string> paymentGroupRepository)
        {
            _paymentGroupRepository = paymentGroupRepository;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(EditPaymentGroupApprovementRequest.EndpointUri)]
        public override async Task<ActionResult<EditPaymentGroupApprovementRequest.Respone>> HandleAsync(EditPaymentGroupApprovementRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var paymentGroup = await _paymentGroupRepository.FindByIdAsync(request.PaymentGroupId);
                if (paymentGroup != null)
                {
                    if (paymentGroup.IsPaymentCheckPending)
                    {
                        return Ok(new EditPaymentGroupApprovementRequest.Respone(WasSauccessful: false));
                    }
                    paymentGroup.Message = "Verification pending";
                    paymentGroup.IsPaymentCheckPending = true;
                    paymentGroup.LastUpdateDateTime = DateTime.UtcNow;
                    _paymentGroupRepository.Update(paymentGroup);
                    return Ok(new EditPaymentGroupApprovementRequest.Respone(WasSauccessful: true));
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
