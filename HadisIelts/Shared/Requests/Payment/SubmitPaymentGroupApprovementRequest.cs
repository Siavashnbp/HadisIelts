using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Payment
{
    public record SubmitPaymentGroupApprovementRequest(string PaymentGroupId, bool IsApproved)
        : IRequest<SubmitPaymentGroupApprovementRequest.Response>
    {
        public const string EndpointUri = "/api/payment/ApprovePaymentGroup";
        public record Response(bool WasSuccessful, string Message);
    }
    public class SubmitPaymentGroupApprovementRequestValidator
        : AbstractValidator<SubmitPaymentGroupApprovementRequest>
    {
        public SubmitPaymentGroupApprovementRequestValidator()
        {
            RuleFor(x => x.PaymentGroupId).NotEmpty().NotNull();
        }
    }
}
