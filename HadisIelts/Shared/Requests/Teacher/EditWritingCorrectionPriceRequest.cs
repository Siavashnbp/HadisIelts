using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record EditWritingCorrectionPriceRequest(WritingCorrectionServicePriceSharedModel WritingCorrectionPrice)
        : IRequest<EditWritingCorrectionPriceRequest.Response>
    {
        public const string EndpointUri = "/api/teacher/editWritingCorrectionPrice";
        public record Response(WritingCorrectionServicePriceSharedModel UpdatedWritingCorrectionPrice);
    }
    public class UpdateWritingCorrectionServicePriceRequestValidator
        : AbstractValidator<WritingCorrectionServicePriceSharedModel>
    {
        public UpdateWritingCorrectionServicePriceRequestValidator()
        {
            RuleFor(x => x.ID).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Price).NotNull().NotEmpty();
            RuleFor(x => x.Price).GreaterThan((uint)0);
            RuleFor(x => x.WordCount).NotNull().NotEmpty();
            RuleFor(x => x.WordCount).GreaterThan(0);
            RuleFor(x => x.WritingTypeID).NotNull().NotEmpty();
        }
    }
}
