using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class SubmitPaymentGroupApprovementRequestHandler : BaseMediatorRequestHandler
        <SubmitPaymentGroupApprovementRequest, SubmitPaymentGroupApprovementRequest.Response>
    {
        public SubmitPaymentGroupApprovementRequestHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(SubmitPaymentGroupApprovementRequest.EndpointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
