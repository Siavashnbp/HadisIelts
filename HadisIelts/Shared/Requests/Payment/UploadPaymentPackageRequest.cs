using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Payment
{
    public record UploadPaymentPackageRequest(List<PaymentPictureSharedModel> PaymentPictures,
        string PaymentId)
        : IRequest<UploadPaymentPackageRequest.Response>
    {
        public const string EndpointUri = "/api/payment/submitWritingCorrectionPayment";
        public record Response(List<PaymentPictureSharedModel> PaymentPictures) : ServerResponse;
    }
    public class UploadPaymentPackageRequestValidator : AbstractValidator<UploadPaymentPackageRequest>
    {
        public UploadPaymentPackageRequestValidator()
        {
            RuleFor(x => x.PaymentId).NotEmpty().NotNull();
            RuleFor(x => x.PaymentPictures).NotEmpty().NotNull();
            RuleFor(x => x.PaymentPictures.Count).GreaterThan(0);
            RuleForEach(x => x.PaymentPictures).ChildRules(picture =>
            {
                picture.RuleFor(y => y.Data).NotNull().NotEmpty();
                picture.RuleFor(y => y.Name).NotNull().NotEmpty();
                picture.RuleFor(y => y.FileSuffix).NotNull().NotEmpty();
            });
        }
    }
}
