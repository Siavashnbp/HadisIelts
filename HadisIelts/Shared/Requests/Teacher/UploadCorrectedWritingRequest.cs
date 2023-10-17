using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record UploadCorrectedWritingRequest(int WritingFileId, string Name, string Data, string ContentType)
        : IRequest<UploadCorrectedWritingRequest.Response>
    {
        public const string EndpointUri = "/api/services/writingCorrection/uploadCorrectedFile";
        public record Response(CorrectedWritingSharedModel CorrectedFile);
    }
    public class UploadCorrectedWritingRequestValidator : AbstractValidator<UploadCorrectedWritingRequest>
    {
        public UploadCorrectedWritingRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.WritingFileId).NotEmpty().NotNull();
            RuleFor(x => x.WritingFileId).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Data).NotEmpty().NotNull();
        }
    }
}
