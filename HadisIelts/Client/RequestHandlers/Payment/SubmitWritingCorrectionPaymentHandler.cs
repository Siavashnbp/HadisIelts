using HadisIelts.Shared.Requests.Payment;
using MediatR;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class SubmitWritingCorrectionPaymentHandler :
        IRequestHandler<SubmitCorrectionPaymentRequest, SubmitCorrectionPaymentRequest.Response>
    {
        private readonly HttpClient _httpClient;
        public SubmitWritingCorrectionPaymentHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<SubmitCorrectionPaymentRequest.Response> Handle(SubmitCorrectionPaymentRequest request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsJsonAsync
                (SubmitCorrectionPaymentRequest.EndpointUri, request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var submittedPayment = await response.Content.
                    ReadFromJsonAsync<PaymentResponse>(cancellationToken: cancellationToken);
                return new SubmitCorrectionPaymentRequest.Response(submittedPayment);
            }
            else
            {
                return new SubmitCorrectionPaymentRequest.Response
                    (new PaymentResponse { PaymentID = string.Empty, ErrorMessage = response.ReasonPhrase });
            }

        }
    }
}
