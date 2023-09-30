using FluentValidation;
using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Correction
{
    public record UploadProcessedWritingFilesRequest(WritingCorrectionPackageSharedModel WritingCorrectionPackage)
        : IRequest<UploadProcessedWritingFilesRequest.Response>
    {
        public const string EndpointUri = "/api/services/submitProcessedWritingFiles";
        public record Response(string PaymentId) : ServerResponse;
    }
    public class UploadProcessedWritingFilesRequestValidator
        : AbstractValidator<UploadProcessedWritingFilesRequest>
    {
        public UploadProcessedWritingFilesRequestValidator()
        {
            RuleFor(x => x.WritingCorrectionPackage).SetValidator(new WritingCorrectionPackageValidator());
        }
    }
    public class WritingCorrectionPackageValidator
        : AbstractValidator<WritingCorrectionPackageSharedModel>
    {
        public WritingCorrectionPackageValidator()
        {
            RuleFor(x => x.TotalPrice).GreaterThan((uint)0);
            RuleFor(x => x.ProcessedWritingFiles).NotNull().NotEmpty();
            RuleFor(x => x.ProcessedWritingFiles.Count).GreaterThan(0);
        }
    }


}
