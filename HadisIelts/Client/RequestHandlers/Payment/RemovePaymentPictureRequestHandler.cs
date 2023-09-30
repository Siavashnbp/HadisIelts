using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class RemovePaymentPictureRequestHandler : BaseMediatorRequestHandler
        <RemovePaymentPictureRequest, RemovePaymentPictureRequest.Response>
    {
        public RemovePaymentPictureRequestHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(RemovePaymentPictureRequest.EndpointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
