using HadisIelts.Shared.Models;
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

    }
    public class CalculatedPayment
    {
        public List<ProcessedWritingFile> ProcessedFiles { get; set; }
        public uint TotalPrice { get; set; }
        public string Message { get; set; }
    }
    public class ProcessedWritingFile
    {
        public WritingFile WritingFile { get; set; }
        public int WordCount { get; set; }
        public PriceGroup PriceGroup { get; set; }
        public string Message { get; set; }

    }
    public class PriceGroup
    {
        public string PriceName { get; set; }
        public uint Price { get; set; }
    }
}
