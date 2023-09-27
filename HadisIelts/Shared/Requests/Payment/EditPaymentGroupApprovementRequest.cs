using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Payment
{
    public record EditPaymentGroupApprovementRequest(string PaymentGroupId)
        : IRequest<EditPaymentGroupApprovementRequest.Respone>
    {
        public const string EndpointUri = "/api/paymentGroup/EeditPaymentGroupApprovement";
        public record Respone(bool WasSauccessful);
    }
    public class EditPaymentGroupApprovementRequestValidator : AbstractValidator<EditPaymentGroupApprovementRequest>
    {
        public EditPaymentGroupApprovementRequestValidator()
        {
            RuleFor(x => x.PaymentGroupId).NotEmpty().NotNull();
        }
    }
}
