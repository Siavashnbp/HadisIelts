using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Teacher;
using System.Net;

namespace HadisIelts.Client.RequestHandlers.Teacher
{
    public class AddWritingCorrectionServicePriceHandler : BaseMediatorRequestHandler
        <AddWritingCorrectionPriceRequest, AddWritingCorrectionPriceRequest.Response>
    {
        public AddWritingCorrectionServicePriceHandler(HttpClient httpClient)
            : base(httpClient, AddWritingCorrectionPriceRequest.EndPointUri)
        {
        }
        public override AddWritingCorrectionPriceRequest.Response HandleError(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return new AddWritingCorrectionPriceRequest.Response(new WritingCorrectionServicePriceSharedModel
                {
                    Id = -409
                });
            }
            return null!;
        }
    }
}
