using HadisIelts.Shared.Requests;
using HadisIelts.Shared.Requests.Teacher;
using System.Net;
using System.Net.Http.Json;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class UploadCorrectedWritingRequestHandler : BaseMediatorRequestHandler
        <UploadCorrectedWritingRequest, UploadCorrectedWritingRequest.Response>
    {
        public UploadCorrectedWritingRequestHandler(HttpClient httpClient)
            : base(httpClient, UploadCorrectedWritingRequest.EndpointUri)
        {
        }
        public override async Task<UploadCorrectedWritingRequest.Response> HandleError(HttpResponseMessage response)
        {
            var result = new UploadCorrectedWritingRequest.Response(CorrectedFile: null!);
            var serverResponse = await response.Content.ReadFromJsonAsync<ServerResponse>();
            if (serverResponse?.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.StatusCode = HttpStatusCode.Unauthorized;
                result.Message = serverResponse.Message;
            }
            return result;
        }
    }
}
