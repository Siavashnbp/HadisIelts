using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class GetPaymentGroupHandler : BaseMediatorRequestHandler
        <GetPaymentGroupRequest, GetPaymentGroupRequest.Response>
    {
        public GetPaymentGroupHandler() : base(GetPaymentGroupRequest.EndpointUri)
        {
        }
    }
}
