using MediatR;

namespace HadisIelts.Shared.Requests.Correction
{
    public record GetWritingTypesRequest()
        : IRequest<GetWritingTypesRequest.Response>
    {
        public const string EndPointUri = "/api/services/getWritingTypes";
        public record Response(List<WritingType> WritingTypes);
    }
    public class WritingType
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
