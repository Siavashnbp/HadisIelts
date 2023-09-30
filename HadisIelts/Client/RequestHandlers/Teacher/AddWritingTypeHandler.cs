using HadisIelts.Shared.ErrorHandling.HttpResponseHandling;
using HadisIelts.Shared.Requests.Teacher;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class AddWritingTypeHandler : BaseMediatorRequestHandler
        <AddWritingTypeRequest, AddWritingTypeRequest.Response>
    {
        public AddWritingTypeHandler(HttpClient httpClient, IHttpResponseHandler httpResponseHandler)
            : base(AddWritingTypeRequest.EndPointUri, httpClient, httpResponseHandler)
        {
        }
    }
}
