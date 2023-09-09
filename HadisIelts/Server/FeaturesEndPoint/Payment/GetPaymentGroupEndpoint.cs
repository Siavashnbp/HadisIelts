using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Payment
{
    public class GetPaymentGroupEndpoint : EndpointBaseAsync
        .WithRequest<GetPaymentGroupRequest>
        .WithActionResult<GetPaymentGroupRequest.Response>
    {
        private readonly ICustomRepositoryServices<PaymentGroup, string> _paymentGroupRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly HttpClient _httpClient;
        public GetPaymentGroupEndpoint(ICustomRepositoryServices<PaymentGroup, string> paymentGroupRepository,
            ApplicationDbContext dbContext,
            HttpClient httpClient)
        {
            _paymentGroupRepository = paymentGroupRepository;
            _dbContext = dbContext;
            _httpClient = httpClient;
        }
        [Authorize]
        [HttpPost(GetPaymentGroupRequest.EndpointUri)]
        public override async Task<ActionResult<GetPaymentGroupRequest.Response>> HandleAsync(GetPaymentGroupRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var paymentGroup = await _paymentGroupRepository.FindByIDAsync(request.PaymentID);
                if (paymentGroup is not null)
                {
                    var pictures = _dbContext.PaymentPictures.Where(x => x.PaymentGroupID == paymentGroup.ID).ToList();
                    var payments = new List<PaymentPictureSharedModel>();
                    foreach (var item in pictures)
                    {
                        payments.Add(new PaymentPictureSharedModel
                        {
                            Data = item.Data,
                            FileSuffix = item.FileSuffix,
                            ID = item.ID,
                            IsVerified = item.IsVerified,
                            Message = item.Message,
                            Name = item.Name,
                            UploadDateTime = paymentGroup.LastUpdateDateTime
                        });
                    }
                    return Ok(new GetPaymentGroupRequest.Response(
                        new PaymentGroupSharedModel<WritingCorrectionPackageSharedModel>
                        {
                            UploadDateTime = paymentGroup.LastUpdateDateTime,
                            IsPaymentApproved = paymentGroup.IsPaymentApproved,
                            Message = paymentGroup.Message,
                            IsPaymentCheckPending = paymentGroup.IsPaymentCheckPending,
                            PaymentPictures = payments,
                            ID = paymentGroup.ID,
                            SubmittedServiceID = paymentGroup.SubmittedServiceID,
                        }));
                }
                return Ok(new GetPaymentGroupRequest.Response(
                    new PaymentGroupSharedModel<WritingCorrectionPackageSharedModel>
                    {
                        Message = "Payment Group wan not found"
                    }));
            }
            catch (Exception)
            {

                throw;
            }
            throw new NotImplementedException();
        }
    }
}
