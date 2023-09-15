using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class PaymentVerificationRequestHandler : BaseMediatorRequestHandler
        <PaymentVerificationRequest, PaymentVerificationRequest.Response>
    {
        public PaymentVerificationRequestHandler(HttpClient httpClient)
            : base(httpClient, PaymentVerificationRequest.EndpointUri)
        {
        }
    }
}
