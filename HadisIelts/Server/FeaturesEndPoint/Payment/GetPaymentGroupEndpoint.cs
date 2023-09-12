using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Server.Services.User;
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
        private readonly IUserServices _userServices;
        public GetPaymentGroupEndpoint(ICustomRepositoryServices<PaymentGroup, string> paymentGroupRepository,
            ApplicationDbContext dbContext,
            IUserServices userServices)
        {
            _paymentGroupRepository = paymentGroupRepository;
            _dbContext = dbContext;
            _userServices = userServices;
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
                    if (_userServices.IsUserOwnerOrSpecificRoles(userID: paymentGroup.UserID, claims: User.Claims.ToList(),
                        roles: new List<string> { "Administrator", "Teacher" }))
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
                    return Unauthorized();
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
