using FluentValidation;
using HadisIelts.Shared.Models;

namespace HadisIelts.Client.Features.Teacher.Models
{
    public class WritingPriceModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Price { get; set; }
        public int? WordCount { get; set; }
        private WritingTypeSharedModel _writingType;

        public WritingTypeSharedModel WritingType
        {
            get { return _writingType; }
            set
            {
                _writingType = new WritingTypeSharedModel
                {
                    ID = value.ID,
                    Name = value.Name,
                };
            }
        }
        public WritingPriceModel()
        {
            WritingType = new WritingTypeSharedModel();
        }


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
            RuleFor(x => x.WritingType).NotNull().NotEmpty()
                .WithMessage("Please select a writing type");
            RuleFor(x => x.WritingType.ID).NotEqual(0)
                .WithMessage("Please select a writing type");
        }
    }
}
