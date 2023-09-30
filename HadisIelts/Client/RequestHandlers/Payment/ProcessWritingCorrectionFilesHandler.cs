using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.RequestHandlers.Payment
{
    public class ProcessWritingCorrectionFilesHandler : BaseMediatorRequestHandler
        <ProcessWritingFilesRequest, ProcessWritingFilesRequest.Response>
    {
        public ProcessWritingCorrectionFilesHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(ProcessWritingFilesRequest.EndPointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
