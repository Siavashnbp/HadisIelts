using FluentValidation;
using MediatR;

namespace HadisIelts.Shared.Requests.Correction
{
    public record SubmitWritingCorrectionRequest(WritingCorrectionRequest Request)
        : IRequest<SubmitWritingCorrectionRequest.Response>
    {
        public const string EndpointUri = "/api/submitWritingCorrection";
        public record Response(int SubmittedRequestID);
    }
    public class WritingCorrectionRequest
    {
        public bool RespondViaEmail { get; set; }
        public List<WritingRequestFile> RequestFiles { get; set; } = new();

    }
    public class WritingRequestFile
    {
        public string Name { get; set; }
        public string Data { get; set; }
    }
    public class WritingCorrectionValidator : AbstractValidator<SubmitWritingCorrectionRequest>
    {
        public WritingCorrectionValidator()
        {
            RuleFor(x => x.Request).SetValidator(new WritingRequestValidator());

        }
    }
    public class WritingRequestValidator : AbstractValidator<WritingCorrectionRequest>
    {
        public WritingRequestValidator()
        {
            RuleForEach(x => x.RequestFiles).SetValidator(new WritingFileValidator());
        }
    }
    public class WritingFileValidator : AbstractValidator<WritingRequestFile>
    {
        public WritingFileValidator()
        {
            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Data).NotNull();
        }
    }
}
