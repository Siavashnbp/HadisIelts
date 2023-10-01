using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class EditPaymentPictureVerificationRequestHandler : BaseMediatorRequestHandler
        <EditPaymentPictureVerificationRequest, EditPaymentPictureVerificationRequest.Response>
    {
        public EditPaymentPictureVerificationRequestHandler() : base(EditPaymentPictureVerificationRequest.EndpointUri)
        {
        }
    }
}
