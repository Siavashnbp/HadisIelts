using HadisIelts.Shared.Requests.Correction;
using MediatR;

namespace HadisIelts.Shared.Requests.Payment
{
    public record ProcessWritingFilesRequest(ProcessFilesRequest Request)
        : IRequest<ProcessWritingFilesRequest.Response>
    {
        public const string EndPointUri = "/api/services/payment/processWritingFile";
        public record Response(CalculatedPayment CalculatedPayment);
    }
    public class ProcessFilesRequest
    {
        public List<WritingFile> WritingFiles { get; set; }
        public bool RequiresEmailResponse { get; set; }

    }
    public class CalculatedPayment
    {
        public List<ProcessedWritingFile> ProcessedFiles { get; set; }
        public string Message { get; set; }
    }
    public class ProcessedWritingFile
    {
        public WritingFile WritingFile { get; set; }
        public int WordCount { get; set; }
        public decimal Price { get; set; }

    }
}
