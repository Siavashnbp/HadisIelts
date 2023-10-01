using HadisIelts.Shared.Requests.Teacher;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class DeleteCorrectedRequestHandler : BaseMediatorRequestHandler
        <DeleteCorrectedWritingRequest, DeleteCorrectedWritingRequest.Response>
    {
        public DeleteCorrectedRequestHandler(HttpClient httpClient)
            : base(httpClient, DeleteCorrectedWritingRequest.EndpointUri)
        {
        }
    }
}
