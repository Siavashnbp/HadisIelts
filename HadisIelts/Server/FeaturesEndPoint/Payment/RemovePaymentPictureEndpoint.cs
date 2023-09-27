using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Payment
{
    public class RemovePaymentPictureEndpoint : EndpointBaseAsync
        .WithRequest<RemovePaymentPictureRequest>
        .WithActionResult<RemovePaymentPictureRequest.Response>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserServices _userServices;
        public RemovePaymentPictureEndpoint(ApplicationDbContext dbContext,
            IUserServices userServices)
        {
            _userServices = userServices;
            _dbContext = dbContext;
        }
        [Authorize]
        [HttpPost(RemovePaymentPictureRequest.EndpointUri)]
        public override async Task<ActionResult<RemovePaymentPictureRequest.Response>> HandleAsync(RemovePaymentPictureRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var payment = await _dbContext.PaymentPictures.FindAsync(request.PaymentId);
                if (payment != null && !payment.IsVerified)
                {
                    var paymentGroup = await _dbContext.PaymentGroups.FindAsync(payment.PaymentGroupId);
                    if (paymentGroup != null)
                    {
                        if (_userServices.IsUserOwnerOrSpecificRoles(
                            claims: User.Claims.ToList(), new List<string> { "Administrator", "Teacher" }, paymentGroup.UserId))
                        {
                            _dbContext.PaymentPictures.Remove(payment);
                            var changes = _dbContext.SaveChanges();
                            if (changes > 0)
                            {
                                return Ok(new RemovePaymentPictureRequest.Response(true));
                            }
                        }
                        return Unauthorized();
                    }
                    return Problem("Payment could not be removed");
                }
                return Problem("Payment was not found");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
