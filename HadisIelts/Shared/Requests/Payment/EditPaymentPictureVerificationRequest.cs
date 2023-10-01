using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Payment
{
    public record EditPaymentPictureVerificationRequest(int PictureId)
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
            RuleFor(x => x.PictureId).NotEmpty().NotNull();
            RuleFor(x => x.PictureId).GreaterThanOrEqualTo(0);
        }
    }
}
