using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record AddWritingCorrectionPriceRequest(WritingCorrectionPrice Request)
        : IRequest<AddWritingCorrectionPriceRequest.Response>
    {
        public const string EndPointUri = "/api/teacher/addWritingCorrectionPrice";
        public record Response(WritingCorrectionPrice WritingCorrectionPrice);
    }
    public class WritingCorrectionPrice
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int WritingTypeID { get; set; }
        public int WordCount { get; set; }
        public uint Price { get; set; }
    }

    public class WritingPriceModelValidator : AbstractValidator<WritingCorrectionPrice>
    {
        public WritingPriceModelValidator()
        {
            RuleFor(x => x.Name).NotNull()
                .WithMessage("Name cannot be empty");
            RuleFor(x => x.Price).NotNull().NotEmpty()
                .WithMessage("Price cannot be empty");
            RuleFor(x => x.WordCount).NotNull().NotEmpty()
                .WithMessage("Word count cannot be empty");
            RuleFor(x => x.WordCount).GreaterThan(0)
                .WithMessage("Word count must be greater than 0");
            RuleFor(x => x.WritingTypeID).NotNull().NotEmpty()
                .WithMessage("Please select a writing type");
        }
    }
}
