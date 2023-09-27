using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record DeleteCorrectedWritingRequest(int Id) : IRequest<DeleteCorrectedWritingRequest.Response>
    {
        public const string EndpointUri = "/api/teacher/deleteCorrectedFile";
        public record Response(bool WasSuccessful);
    }
    public class DeleteCorrectedWritingRequestValidator : AbstractValidator<DeleteCorrectedWritingRequest>
    {
        public DeleteCorrectedWritingRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
        }
    }
}
