using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Payment
{
    public record UploadPaymentPackageRequest(List<PaymentPictureSharedModel> PaymentPictures,
        string PaymentID)
        : IRequest<UploadPaymentPackageRequest.Response>
    {
        public const string EndpointUri = "/api/payment/submitWritingCorrectionPayment";
        public record Response(List<PaymentPictureSharedModel> PaymentPictures, string Message);
    }
    public class UploadPaymentPackageRequestValidator : AbstractValidator<UploadPaymentPackageRequest>
    {
        public UploadPaymentPackageRequestValidator()
        {
            RuleFor(x => x.PaymentID).NotEmpty().NotNull();
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
