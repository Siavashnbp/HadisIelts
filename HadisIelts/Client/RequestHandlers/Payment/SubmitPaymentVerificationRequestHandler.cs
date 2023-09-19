using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class SubmitPaymentVerificationRequestHandler : BaseMediatorRequestHandler
        <SubmitPaymentVerificationRequest, SubmitPaymentVerificationRequest.Response>
    {
        public SubmitPaymentVerificationRequestHandler(HttpClient httpClient)
            : base(httpClient, SubmitPaymentVerificationRequest.EndpointUri)
        {
        }
    }
}
