using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class SubmitWritingCorrectionPaymentsHandler : BaseMediatorRequestHandler
        <UploadPaymentPackageRequest, UploadPaymentPackageRequest.Response>
    {
        public SubmitWritingCorrectionPaymentsHandler(HttpClient httpClient)
            : base(httpClient, UploadPaymentPackageRequest.EndpointUri)
        {
        }
        public override UploadPaymentPackageRequest.Response HandleError(HttpResponseMessage response)
        {
            return new UploadPaymentPackageRequest.Response(
                Message: response.ReasonPhrase,
                PaymentPictures: new List<PaymentPictureSharedModel>()
            );
        }
    }
}
