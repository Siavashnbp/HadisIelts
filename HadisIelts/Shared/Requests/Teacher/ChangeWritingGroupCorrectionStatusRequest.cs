using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record ChangeWritingGroupCorrectionStatusRequest(string Id)
        : IRequest<ChangeWritingGroupCorrectionStatusRequest.Response>
    {
        public const string EndpointUri = "/api/services/teacher/writingCorrection/changeGroupCorrectionStatus";
        public record Response(bool CorrectionStatus) : ServerResponse;
    }
    public class ChangeWritingGroupCorrectionStatusRequestValidator
        : AbstractValidator<ChangeWritingGroupCorrectionStatusRequest>
    {
        public ChangeWritingGroupCorrectionStatusRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}
