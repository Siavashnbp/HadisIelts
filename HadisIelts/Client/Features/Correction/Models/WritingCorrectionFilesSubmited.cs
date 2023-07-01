using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace HadisIelts.Client.Features.Correction.Models
{
    public class WritingCorrectionFilesSubmited
    {
        public WritingCorrectionFilesSubmited()
        {
            SubmitedFiles = new();
        }
        public List<WritingFile> SubmitedFiles { get; set; }
        public bool RequiresEmailResponse { get; set; }
        public class WritingFile
        {
            public int ID { get; set; }
            public IBrowserFile File { get; set; }
        }
    }
    public class SubmitedFilesValidator : AbstractValidator<WritingCorrectionFilesSubmited>
    {
        public SubmitedFilesValidator()
        {
            RuleFor(x => x.SubmitedFiles).Must(x => x.Count <= 5).WithMessage("Please select up to 5 files");
            RuleFor(x => x.SubmitedFiles).NotEmpty();
            RuleForEach(x => x.SubmitedFiles).SetValidator(new WritingFileValidator());
        }
    }
    public class WritingFileValidator :
        AbstractValidator<WritingCorrectionFilesSubmited.WritingFile>
    {
        public WritingFileValidator()
        {
            RuleFor(x => x.File).ChildRules(file =>
            {
                RuleFor(x => x.File).Must(f => f.Size < 10E6).WithMessage("Max file size allowed is 10MB");
            });
        }
    }
}
