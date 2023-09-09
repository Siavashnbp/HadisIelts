using HadisIelts.Shared.Models;
using MediatR;

namespace HadisIelts.Shared.Requests.Teacher
{
    public record AddWritingTypeRequest(string WritingName)
        : IRequest<AddWritingTypeRequest.Response>
    {
        public const string EndPointUri = "/api/teacher/addWritingType";
        public record Response(WritingTypeSharedModel WritingType);
    }
}
