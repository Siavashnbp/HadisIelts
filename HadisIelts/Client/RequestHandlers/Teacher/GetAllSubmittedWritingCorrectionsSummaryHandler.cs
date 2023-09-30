using HadisIelts.Shared.Requests.Teacher;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class GetAllSubmittedWritingCorrectionsSummaryHandler : BaseMediatorRequestHandler
        <GetAllSubmittedWritingCorrectionsSummaryRequest, GetAllSubmittedWritingCorrectionsSummaryRequest.Response>
    {
        public GetAllSubmittedWritingCorrectionsSummaryHandler() : base(GetAllSubmittedWritingCorrectionsSummaryRequest.EndpointUri)
        {
        }
    }
}
