using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace HadisIelts.Client.Features.Correction.Models
{
    public class UserWritingCorrectionRequest
    {
        public UserWritingCorrectionRequest()
        {
            WritingFiles = new List<UserWritingFile>();
        }
        public List<UserWritingFile> WritingFiles { get; set; }
        public bool IsProcessed { get; set; }
        public bool RequiresEmailResponse { get; set; }
    }
    public class UserWritingFile
    {
        public int ID { get; set; }
        public IBrowserFile BrowserFile { get; set; }
        public int WritingTypeID { get; set; }
    }
    public class SubmitedFilesValidator : AbstractValidator<UserWritingCorrectionRequest>
    {
        public SubmitedFilesValidator()
        {
            RuleFor(x => x.WritingFiles).Must(x => x.Count <= 5).WithMessage("Please select up to 5 files");
            RuleFor(x => x.WritingFiles).NotEmpty();
            RuleForEach(x => x.WritingFiles).SetValidator(new UserWritingFileValidator());
        }
    }
    public class UserWritingFileValidator :
        AbstractValidator<UserWritingFile>
    {
        public UserWritingFileValidator()
        {
            RuleFor(x => x.BrowserFile).ChildRules(file =>
            {
                RuleFor(x => x.BrowserFile).Must(f => f.Size < 10E6).WithMessage("Max file size allowed is 10MB");
            });
        }
    }
}
