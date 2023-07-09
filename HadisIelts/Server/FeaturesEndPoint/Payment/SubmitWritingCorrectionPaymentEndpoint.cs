using Ardalis.ApiEndpoints;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndpoint.Payment
{
    public class SubmitWritingCorrectionPaymentEndpoint : EndpointBaseAsync
        .WithRequest<SubmitCorrectionPaymentRequest>
        .WithResult<PaymentResponse>
    {
        [HttpPost(SubmitCorrectionPaymentRequest.EndpointUri)]
        public override async Task<PaymentResponse> HandleAsync(SubmitCorrectionPaymentRequest request, CancellationToken cancellationToken = default)
        {
            await Task.Delay(500);
            return new PaymentResponse
            {
                PaymentID = "SomeID",
                ErrorMessage = string.Empty,
            };
        }
    }
}
