using HadisIelts.Shared.Requests.Teacher;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class ChangeWritingGroupCorrectionStatusRequestHandler : BaseMediatorRequestHandler
        <ChangeWritingGroupCorrectionStatusRequest, ChangeWritingGroupCorrectionStatusRequest.Response>
    {
        public ChangeWritingGroupCorrectionStatusRequestHandler(HttpClient httpClient)
            : base(httpClient, ChangeWritingGroupCorrectionStatusRequest.EndpointUri)
        {
        }
    }
}
