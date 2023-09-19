using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Payment
{
    public record SubmitPaymentVerificationRequest(int PictureID, bool IsVerfifed)
        : IRequest<SubmitPaymentVerificationRequest.Response>
    {
        public const string EndpointUri = "/api/teacher/payment/verification";
        public record Response(PaymentPictureSharedModel VerifiedPayment);
    }
    public class VerifyingPaymentRequestValidator : AbstractValidator<SubmitPaymentVerificationRequest>
    {
        public VerifyingPaymentRequestValidator()
        {
            RuleFor(x => x.PictureID).NotEmpty().NotNull();
            RuleFor(x => x.PictureID).GreaterThanOrEqualTo(0);
        }
    }
}
