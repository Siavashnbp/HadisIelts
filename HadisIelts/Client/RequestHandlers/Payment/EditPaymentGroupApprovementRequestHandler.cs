using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class EditPaymentGroupApprovementRequestHandler : BaseMediatorRequestHandler
        <EditPaymentGroupApprovementRequest, EditPaymentGroupApprovementRequest.Respone>
    {
        public EditPaymentGroupApprovementRequestHandler() : base(EditPaymentGroupApprovementRequest.EndpointUri)
        {
        }
    }
}
