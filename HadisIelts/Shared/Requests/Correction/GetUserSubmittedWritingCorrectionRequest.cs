using HadisIelts.Shared.Models;
using MediatR;
using System.Net;

namespace HadisIelts.Shared.Requests.Correction
{
    public record GetUserSubmittedWritingCorrectionRequest(string UserId)
        : IRequest<GetUserSubmittedWritingCorrectionRequest.Response>
    {
        public const string EndpointUri = "/api/services/writingCorrection/GetUserSubmissions";
        public record Response(List<SubmittedServiceSummarySharedModel> SubmittedServices, HttpStatusCode StatusCode);
    }
}
