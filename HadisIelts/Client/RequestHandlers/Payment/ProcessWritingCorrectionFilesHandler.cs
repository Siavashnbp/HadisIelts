using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class ProcessWritingCorrectionFilesHandler : BaseMediatorRequestHandler
        <ProcessWritingFilesRequest, ProcessWritingFilesRequest.Response>
    {
        public ProcessWritingCorrectionFilesHandler(HttpClient httpClient)
            : base(httpClient, ProcessWritingFilesRequest.EndPointUri)
        {
        }
    }
}
