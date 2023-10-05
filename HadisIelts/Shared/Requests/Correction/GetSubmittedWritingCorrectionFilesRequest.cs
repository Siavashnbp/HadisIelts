using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Correction
{
    public record GetSubmittedWritingCorrectionFilesRequest(string SubmissionId)
        : IRequest<GetSubmittedWritingCorrectionFilesRequest.Response>
    {
        public const string EndpointUri = "/api/services/writingCorrection/getFiles";
        public record Response(WritingCorrectionPackageSharedModel WritingCorrectionPackage);
    }
    public class GetSubmittedWritingCorrectionFilesRequestValidator
        : AbstractValidator<GetSubmittedWritingCorrectionFilesRequest>
    {
        public GetSubmittedWritingCorrectionFilesRequestValidator()
        {
            RuleFor(x => x.SubmissionId).NotEmpty().NotNull();
        }
    }
}
