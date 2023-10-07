using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;
using System.Net;

namespace HadisIelts.Shared.Requests.Correction
{
    public record GetSubmittedWritingCorrectionFilesRequest(string SubmissionId)
        : IRequest<GetSubmittedWritingCorrectionFilesRequest.Response>
    {
        public const string EndpointUri = "/api/services/writingCorrection/getFiles";
        public record Response(WritingCorrectionPackageSharedModel WritingCorrectionPackage, HttpStatusCode StatusCode);
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
