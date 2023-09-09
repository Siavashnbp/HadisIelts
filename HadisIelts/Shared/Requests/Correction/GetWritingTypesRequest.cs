using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Correction
{
    public record GetWritingTypesRequest()
        : IRequest<GetWritingTypesRequest.Response>
    {
        public const string EndPointUri = "/api/services/getWritingTypes";
        public record Response(List<WritingTypeSharedModel> WritingTypes);
    }
}
