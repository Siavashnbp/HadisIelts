using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class GetPaymentGroupHandler : BaseMediatorRequestHandler
        <GetPaymentGroupRequest, GetPaymentGroupRequest.Response>
    {
        public GetPaymentGroupHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(GetPaymentGroupRequest.EndpointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
