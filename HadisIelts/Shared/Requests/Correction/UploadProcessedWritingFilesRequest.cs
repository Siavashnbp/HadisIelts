namespace HadisIelts.Shared.Requests.Correction
{
    public record UploadProcessedWritingFilesRequest(SubmitProcessedWritingCorrectinFiles Request)
    {
        public const string EndpointUri = "/api/services/submitProcessedWritingFiles";
    }
    public class SubmitProcessedWritingCorrectinFiles
    {
        public bool RequiresEmailResponse { get; set; }
        public List<WritingFile> WritingFiles { get; set; }
        public string UserID { get; set; }
    }
}
