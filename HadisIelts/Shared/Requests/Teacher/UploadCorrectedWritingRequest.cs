using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record UploadCorrectedWritingRequest(int WritingFileID, string Name, string Data)
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
            RuleFor(x => x.WritingFileID).NotEmpty().NotNull();
            RuleFor(x => x.WritingFileID).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Data).NotEmpty().NotNull();
        }
    }
}
