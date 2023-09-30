using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class EditPaymentGroupApprovementRequestHandler : BaseMediatorRequestHandler
        <EditPaymentGroupApprovementRequest, EditPaymentGroupApprovementRequest.Respone>
    {
        public EditPaymentGroupApprovementRequestHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(EditPaymentGroupApprovementRequest.EndpointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
