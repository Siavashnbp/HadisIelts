using HadisIelts.Shared.Requests.Teacher;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class UploadCorrectedWritingRequestHandler : BaseMediatorRequestHandler
        <UploadCorrectedWritingRequest, UploadCorrectedWritingRequest.Response>
    {
        public UploadCorrectedWritingRequestHandler() : base(UploadCorrectedWritingRequest.EndpointUri)
        {
        }
    }
}
