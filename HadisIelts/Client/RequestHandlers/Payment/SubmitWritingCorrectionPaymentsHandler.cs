using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class SubmitWritingCorrectionPaymentsHandler : BaseMediatorRequestHandler
        <UploadPaymentPackageRequest, UploadPaymentPackageRequest.Response>
    {
        public SubmitWritingCorrectionPaymentsHandler() : base(UploadPaymentPackageRequest.EndpointUri)
        {
        }

    }
}
