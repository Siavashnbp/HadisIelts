using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Correction
{
    public record UploadProcessedWritingFilesRequest(SubmitProcessedWritingCorrectinFiles Request)
        : IRequest<UploadProcessedWritingFilesRequest.Response>
    {
        public const string EndpointUri = "/api/services/submitProcessedWritingFiles";
        public record Response(string SubmissionID);
    }
    public class SubmitProcessedWritingCorrectinFiles
    {
        public bool RequiresEmailResponse { get; set; }
        public List<WritingFile> WritingFiles { get; set; }
        public string UserID { get; set; }
        public SubmitProcessedWritingCorrectinFiles()
        {
            WritingFiles = new List<WritingFile>();
        }
    }

}
