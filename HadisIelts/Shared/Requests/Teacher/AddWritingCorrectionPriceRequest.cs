using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record AddWritingCorrectionPriceRequest(WritingCorrectionServicePriceSharedModel WritingCorrectionServicePrice)
        : IRequest<AddWritingCorrectionPriceRequest.Response>
    {
        public const string EndPointUri = "/api/teacher/addWritingCorrectionPrice";
        public record Response(WritingCorrectionServicePriceSharedModel AddedWritingCorrectionServicePrice);
    }

    public class WritingCorrectionServicePriceValidator : AbstractValidator<WritingCorrectionServicePriceSharedModel>
    {
        public WritingCorrectionServicePriceValidator()
        {
            RuleFor(x => x.Id).Null();
            RuleFor(x => x.Name).NotNull().NotEmpty()
                .WithMessage("Name cannot be empty");
            RuleFor(x => x.Price).NotNull().NotEmpty()
                .WithMessage("Price cannot be empty");
            RuleFor(x => x.WordCount).NotNull().NotEmpty()
                .WithMessage("Word count cannot be empty");
            RuleFor(x => x.WordCount).GreaterThan(0)
                .WithMessage("Word count must be greater than 0");
            RuleFor(x => x.WritingTypeId).NotNull().NotEmpty()
                .WithMessage("Please select a writing type");
        }
    }
}
