using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Payment
{
    public record SubmitPaymentVerificationRequest(int PictureId, bool IsVerfifed)
        : IRequest<SubmitPaymentVerificationRequest.Response>
    {
        public const string EndpointUri = "/api/teacher/payment/verification";
        public record Response(PaymentPictureSharedModel VerifiedPayment) : ServerResponse;
    }
    public class VerifyingPaymentRequestValidator : AbstractValidator<SubmitPaymentVerificationRequest>
    {
        public VerifyingPaymentRequestValidator()
        {
            RuleFor(x => x.PictureId).NotEmpty().NotNull();
            RuleFor(x => x.PictureId).GreaterThanOrEqualTo(0);
        }
    }
}
