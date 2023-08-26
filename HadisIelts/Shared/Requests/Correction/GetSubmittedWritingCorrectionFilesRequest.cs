using HadisIelts.Shared.Requests.Payment;
using MediatR;

namespace HadisIelts.Shared.Requests.Correction
{
    public record GetSubmittedWritingCorrectionFilesRequest(SubmittedWritingFilesIdentifications Request)
        : IRequest<GetSubmittedWritingCorrectionFilesRequest.Response>
    {
        public const string EndpointUri = "/api/services/writingCorrection/getFiles";
        public record Response(CalculatedWritingCorrectionPayment CalculatedWritingCorrectionPayment);
    }
    public class SubmittedWritingFilesIdentifications
    {
        public string UserID { get; set; }
        public string SubmissionID { get; set; }
    }
}
