using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class SubmitWritingCorrectionPaymentsHandler : BaseMediatorRequestHandler
        <UploadPaymentPackageRequest, UploadPaymentPackageRequest.Response>
    {
        public SubmitWritingCorrectionPaymentsHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(UploadPaymentPackageRequest.EndpointUri, httpClient, httpResponseHandler)
        {
        }

    }
}
