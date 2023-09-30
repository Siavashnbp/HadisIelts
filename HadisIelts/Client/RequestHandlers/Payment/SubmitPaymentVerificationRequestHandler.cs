using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class SubmitPaymentVerificationRequestHandler : BaseMediatorRequestHandler
        <SubmitPaymentVerificationRequest, SubmitPaymentVerificationRequest.Response>
    {
        public SubmitPaymentVerificationRequestHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(SubmitPaymentVerificationRequest.EndpointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
