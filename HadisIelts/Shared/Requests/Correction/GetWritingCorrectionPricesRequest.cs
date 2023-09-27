using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Correction
{
    public record GetWritingCorrectionPricesRequest()
        : IRequest<GetWritingCorrectionPricesRequest.Response>
    {
        public const string EndPointUri = "/api/services/getWritingCorrectionPrices";
        public record Response(List<WritingCorrectionServicePriceSharedModel> WritingCorrectionPrices);
    }

}
