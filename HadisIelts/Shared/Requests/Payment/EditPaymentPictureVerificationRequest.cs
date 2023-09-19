using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Payment
{
    public record EditPaymentPictureVerificationRequest(int PictureID)
        : IRequest<EditPaymentPictureVerificationRequest.Response>
    {
        public const string EndpointUri = "/api/teacher/payment/editVerification";
        public record Response(bool WasSuccessful);
    }
    public class EditPaymentVerficationRequestValidator
        : AbstractValidator<EditPaymentPictureVerificationRequest>
    {
        public EditPaymentVerficationRequestValidator()
        {
            RuleFor(x => x.PictureID).NotEmpty().NotNull();
            RuleFor(x => x.PictureID).GreaterThanOrEqualTo(0);
        }
    }
}
