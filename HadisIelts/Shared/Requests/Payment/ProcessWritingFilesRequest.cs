using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Payment
{
    public record ProcessWritingFilesRequest(List<WritingFileSharedModel> WritingFiles)
        : IRequest<ProcessWritingFilesRequest.Response>
    {
        public const string EndPointUri = "/api/services/payment/processWritingFile";
        public record Response(WritingCorrectionPackageSharedModel ProcessedWritingCorrection) : ServerResponse;
    }
    public class ProcessWritingFilesRequestValidator
        : AbstractValidator<ProcessWritingFilesRequest>
    {
        public ProcessWritingFilesRequestValidator()
        {
            RuleFor(x => x.WritingFiles).NotNull().NotEmpty();
            RuleFor(x => x.WritingFiles.Count).GreaterThan(0);
        }
    }
}
