using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record AddWritingTypeRequest(string WritingName)
        : IRequest<AddWritingTypeRequest.Response>
    {
        public const string EndPointUri = "/api/teacher/addWritingType";
        public record Response(WritingTypeSharedModel WritingType);
    }
    public class AddWritingTypeRequestValidator : AbstractValidator<AddWritingTypeRequest>
    {
        public AddWritingTypeRequestValidator()
        {
            RuleFor(x => x.WritingName).NotEmpty().NotNull()
                .WithMessage("Writing type name cannot be empty");
        }
    }
}
