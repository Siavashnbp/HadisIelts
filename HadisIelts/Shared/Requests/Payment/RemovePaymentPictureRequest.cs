using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Payment
{
    public record RemovePaymentPictureRequest(int PaymentId) : IRequest<RemovePaymentPictureRequest.Response>
    {
        public const string EndpointUri = "/api/payment/removePayment";
        public record Response(bool WasSuccessful) : ServerResponse;
    }
    public class RemovePaymentPictureRequestValidator : AbstractValidator<RemovePaymentPictureRequest>
    {
        public RemovePaymentPictureRequestValidator()
        {
            RuleFor(x => x.PaymentId).NotEmpty().NotNull();
        }
    }
}
