using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Payment
{
    public record SubmitCorrectionPaymentRequest(PaymentRequest Request)
        : IRequest<SubmitCorrectionPaymentRequest.Response>
    {
        public const string EndpointUri = "/api/submitWritingPayment";
        public record Response(PaymentResponse PaymentResponse);
    }
    public class PaymentRequest
    {
        public string SubmissionID { get; set; }
        public string Data { get; set; }
    }
    public class PaymentResponse
    {
        public string PaymentID { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class SubmitPaymentValidator : AbstractValidator<SubmitCorrectionPaymentRequest>
    {
        public SubmitPaymentValidator()
        {
            RuleFor(x => x.Request).SetValidator(new PaymentValidator());
        }
    }
    public class PaymentValidator : AbstractValidator<PaymentRequest>
    {
        public PaymentValidator()
        {
            RuleFor(x => x.SubmissionID).NotNull().NotEmpty();
            RuleFor(x => x.Data).NotNull().NotEmpty();
        }
    }
}
