using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record AddWritingCorrectionPriceRequest(WritingCorrectionServicePriceSharedModel WritingCorrectionServicePrice)
        : IRequest<AddWritingCorrectionPriceRequest.Response>
    {
        public const string EndPointUri = "/api/teacher/addWritingCorrectionPrice";
        public record Response(WritingCorrectionServicePriceSharedModel AddedWritingCorrectionServicePrice) : ServerResponse;
    }

    public class WritingCorrectionServicePriceValidator : AbstractValidator<WritingCorrectionServicePriceSharedModel>
    {
        public WritingCorrectionServicePriceValidator()
        {
            RuleFor(x => x.Id).Null();
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Price).NotNull().NotEmpty();
            RuleFor(x => x.WordCount).NotNull().NotEmpty();
            RuleFor(x => x.WordCount).GreaterThan(0);
            RuleFor(x => x.WritingTypeId).NotNull().NotEmpty();
        }
    }
}
