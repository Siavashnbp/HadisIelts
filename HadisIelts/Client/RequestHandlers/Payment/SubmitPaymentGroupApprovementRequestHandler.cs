using HadisIelts.Shared.Requests.Payment;
using System.Net;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class SubmitPaymentGroupApprovementRequestHandler : BaseMediatorRequestHandler
        <SubmitPaymentGroupApprovementRequest, SubmitPaymentGroupApprovementRequest.Response>
    {
        public SubmitPaymentGroupApprovementRequestHandler(HttpClient httpClient)
            : base(httpClient, SubmitPaymentGroupApprovementRequest.EndpointUri)
        {
        }
        public override SubmitPaymentGroupApprovementRequest.Response HandleError(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return new SubmitPaymentGroupApprovementRequest.Response(WasSuccessful: false, Message: "Changes could not be saved");
            }
            else if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return new SubmitPaymentGroupApprovementRequest.Response(WasSuccessful: false, Message: "Payment group was not found");
            }
            return null!;
        }
    }
}
