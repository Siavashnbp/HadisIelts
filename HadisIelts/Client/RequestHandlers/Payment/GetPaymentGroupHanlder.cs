using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class GetPaymentGroupHandler : BaseMediatorRequestHandler
        <GetPaymentGroupRequest, GetPaymentGroupRequest.Response>
    {
        public GetPaymentGroupHandler(HttpClient httpClient)
            : base(httpClient, GetPaymentGroupRequest.EndpointUri)
        {
        }
    }
}
