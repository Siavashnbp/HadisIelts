using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record RemoveWritingCorrectionPriceRequest(int ID) : IRequest
        <RemoveWritingCorrectionPriceRequest.Response>
    {
        public const string EndPointUri = "/api/teacher/removeWritingCorrectionPrice";
        public record Response(bool WasSuccessful);
    }
    public class RemoveWritingCorrectionPriceRequestValidator : AbstractValidator<RemoveWritingCorrectionPriceRequest>
    {
        public RemoveWritingCorrectionPriceRequestValidator()
        {
            RuleFor(x => x.ID).NotEmpty().NotNull();
        }
    }
}
