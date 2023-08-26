using HadisIelts.Shared.Requests.Payment;
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
        public uint TotalPrice { get; set; }
        public List<ProcessedWritingFile> ProcessedWritingFiles { get; set; }
        public string UserID { get; set; }
        public SubmitProcessedWritingCorrectinFiles()
        {
            ProcessedWritingFiles = new List<ProcessedWritingFile>();
        }
    }

}
