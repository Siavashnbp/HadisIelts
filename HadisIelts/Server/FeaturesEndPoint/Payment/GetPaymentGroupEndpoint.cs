using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
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
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserServices _userServices;
        public GetPaymentGroupEndpoint(ApplicationDbContext dbContext,
            IUserServices userServices)
        {
            _dbContext = dbContext;
            _userServices = userServices;
        }
        [Authorize]
        [HttpPost(GetPaymentGroupRequest.EndpointUri)]
        public override async Task<ActionResult<GetPaymentGroupRequest.Response>> HandleAsync(GetPaymentGroupRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var paymentGroup = await _dbContext.PaymentGroups.FindAsync(request.PaymentId);
                if (paymentGroup is not null)
                {
                    if (_userServices.IsUserOwnerOrSpecificRoles(userId: paymentGroup.UserId, claims: User.Claims.ToList(),
                        roles: new List<string> { "Administrator", "Teacher" }))
                    {
                        var pictures = _dbContext.PaymentPictures.Where(x => x.PaymentGroupId == paymentGroup.Id).ToList();
                        var payments = new List<PaymentPictureSharedModel>();
                        foreach (var item in pictures)
                        {
                            payments.Add(new PaymentPictureSharedModel
                            {
                                Data = Convert.ToBase64String(item.Data),
                                FileSuffix = item.FileSuffix,
                                Id = item.Id,
                                IsVerified = item.IsVerified,
                                IsVerificationPending = item.IsVerificationPending,
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
                                Id = paymentGroup.Id,
                                SubmittedServiceId = paymentGroup.SubmittedServiceId,
                            })
                        { StatusCode = System.Net.HttpStatusCode.OK });
                    }
                    return Unauthorized();
                }
                return Conflict();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
