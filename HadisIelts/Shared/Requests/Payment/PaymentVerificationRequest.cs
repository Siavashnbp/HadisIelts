using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Payment
{
    public record PaymentVerificationRequest(int PaymentID, bool IsVerfifed)
        : IRequest<PaymentVerificationRequest.Response>
    {
        public const string EndpointUri = "/api/teacher/paymentVerification";
        public record Response(PaymentPictureSharedModel VerifiedPayment);
    }
    public class VerifyingPaymentRequestValidator : AbstractValidator<PaymentVerificationRequest>
    {
        public VerifyingPaymentRequestValidator()
        {
            RuleFor(x => x.PaymentID).NotEmpty().NotNull();
        }
    }
}
