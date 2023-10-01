using HadisIelts.Shared.Requests.Teacher;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class ChangeWritingGroupCorrectionStatusRequestHandler : BaseMediatorRequestHandler
        <ChangeWritingGroupCorrectionStatusRequest, ChangeWritingGroupCorrectionStatusRequest.Response>
    {
        public ChangeWritingGroupCorrectionStatusRequestHandler() : base(ChangeWritingGroupCorrectionStatusRequest.EndpointUri)
        {
        }
    }
}
