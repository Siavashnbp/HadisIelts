using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record RemoveWritingCorrectionPriceRequest(int Id) : IRequest
        <RemoveWritingCorrectionPriceRequest.Response>
    {
        public const string EndPointUri = "/api/teacher/removeWritingCorrectionPrice";
        public record Response(bool WasSuccessful) : ServerResponse;
    }
    public class RemoveWritingCorrectionPriceRequestValidator : AbstractValidator<RemoveWritingCorrectionPriceRequest>
    {
        public RemoveWritingCorrectionPriceRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}
