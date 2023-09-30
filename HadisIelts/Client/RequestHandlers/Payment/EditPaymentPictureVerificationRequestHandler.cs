using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class EditPaymentPictureVerificationRequestHandler : BaseMediatorRequestHandler
        <EditPaymentPictureVerificationRequest, EditPaymentPictureVerificationRequest.Response>
    {
        public EditPaymentPictureVerificationRequestHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(EditPaymentPictureVerificationRequest.EndpointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
