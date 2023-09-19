using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Payment
{
    public record RemovePaymentPictureRequest(int PaymentID) : IRequest<RemovePaymentPictureRequest.Response>
    {
        public const string EndpointUri = "/api/payment/removePayment";
        public record Response(bool WasSuccessful);
    }
    public class RemovePaymentPictureRequestValidator : AbstractValidator<RemovePaymentPictureRequest>
    {
        public RemovePaymentPictureRequestValidator()
        {
            RuleFor(x => x.PaymentID).NotEmpty().NotNull();
        }
    }
}
