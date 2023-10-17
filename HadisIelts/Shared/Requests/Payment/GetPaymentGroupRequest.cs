using HadisIelts.Shared.Models;
using MediatR;
using System.Net;

namespace HadisIelts.Shared.Requests.Payment
{
    public record GetPaymentGroupRequest(string PaymentId) : IRequest<GetPaymentGroupRequest.Response>
    {
        public const string EndpointUri = "/api/getWritingCorrectionGroup/paymentId";
        public record Response(PaymentGroupSharedModel<WritingCorrectionPackageSharedModel> PaymentGroup)
        {
            public HttpStatusCode StatusCode { get; set; }
        }
    }
}
