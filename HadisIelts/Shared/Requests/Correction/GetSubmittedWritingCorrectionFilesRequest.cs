using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Correction
{
    public record GetSubmittedWritingCorrectionFilesRequest(string UserID, string SubmissionID)
        : IRequest<GetSubmittedWritingCorrectionFilesRequest.Response>
    {
        public const string EndpointUri = "/api/services/writingCorrection/getFiles";
        public record Response(WritingCorrectionPackageSharedModel WritingCorrectionPackage, string Message);
    }
    public class GetSubmittedWritingCorrectionFilesRequestValidator
        : AbstractValidator<GetSubmittedWritingCorrectionFilesRequest>
    {
        public GetSubmittedWritingCorrectionFilesRequestValidator()
        {
            RuleFor(x => x.UserID).NotEmpty().NotNull();
            RuleFor(x => x.SubmissionID).NotEmpty().NotNull();
        }
    }
}
