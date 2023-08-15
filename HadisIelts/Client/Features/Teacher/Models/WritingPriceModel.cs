using FluentValidation;
using static HadisIelts.Shared.Enums.FileRelatedEnums;

namespace HadisIelts.Client.Features.Teacher.Models
{
    public class WritingPriceModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? WordCount { get; set; }
        public WritingTypes WritingType { get; set; }
    }
    public class WritingPriceModelValidator : AbstractValidator<WritingPriceModel>
    {
        public WritingPriceModelValidator()
        {
            RuleFor(x => x.Name).NotNull()
                .WithMessage("Name cannot be empty");
            RuleFor(x => x.Price).NotNull().NotEmpty()
                .WithMessage("Price cannot be empty");
            RuleFor(x => x.Price).GreaterThan(0)
                .WithMessage("Price must be greater than 0");
            RuleFor(x => x.WordCount).NotNull().NotEmpty()
                .WithMessage("Word count cannot be empty");
            RuleFor(x => x.WordCount).GreaterThan(0)
                .WithMessage("Word count must be greater than 0");
            RuleFor(x => x.WritingType).NotEqual(WritingTypes.NotSelected)
                .WithMessage("Please select a writing type");
        }
    }
}
