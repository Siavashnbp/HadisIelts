using HadisIelts.Shared.Requests.Correction;
using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record AddWritingTypeRequest(string WritingName)
        : IRequest<AddWritingTypeRequest.Response>
    {
        public const string EndPointUri = "/api/teacher/addWritingType";
        public record Response(WritingType WritingType);
    }
}
