using HadisIelts.Shared.Requests.Payment;
using System.Net;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class GetPaymentGroupHandler : BaseMediatorRequestHandler
        <GetPaymentGroupRequest, GetPaymentGroupRequest.Response>
    {
        public GetPaymentGroupHandler(HttpClient httpClient)
            : base(httpClient, GetPaymentGroupRequest.EndpointUri)
        {
        }
        public override GetPaymentGroupRequest.Response HandleError(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new GetPaymentGroupRequest.Response(null!)
                {
                    StatusCode = HttpStatusCode.Unauthorized
                };
            }
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return new GetPaymentGroupRequest.Response(null!)
                {
                    StatusCode = HttpStatusCode.Conflict
                };
            }
            return new GetPaymentGroupRequest.Response(null!)
            {
                StatusCode = HttpStatusCode.BadRequest
            };

        }
    }
}
