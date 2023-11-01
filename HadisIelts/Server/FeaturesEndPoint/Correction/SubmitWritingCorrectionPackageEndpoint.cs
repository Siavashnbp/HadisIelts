using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.Telegram;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Requests.Correction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndpoint.Correction
{
    public class SubmitWritingCorrectionPackageEndpoint : EndpointBaseAsync
        .WithRequest<UploadProcessedWritingFilesRequest>
        .WithActionResult<UploadProcessedWritingFilesRequest.Response>
    {
        private readonly IUserServices _userServices;
        private readonly ApplicationDbContext _dbContext;
        private readonly ITelegramServices _telegramServices;
        private readonly UserManager<ApplicationUser> _userManager;
        public SubmitWritingCorrectionPackageEndpoint(ApplicationDbContext dbContext,
            IUserServices userServices,
            ITelegramServices telegramServices,
            UserManager<ApplicationUser> userManager)

        {
            _dbContext = dbContext;
            _userServices = userServices;
            _telegramServices = telegramServices;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost(UploadProcessedWritingFilesRequest.EndpointUri)]
        public override async Task<ActionResult<UploadProcessedWritingFilesRequest.Response>> HandleAsync(UploadProcessedWritingFilesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _userServices.GetUserIdFromClaims(User.Claims.ToList());
                if (_userServices.HasWritingCorrectionPending(_dbContext, userId))
                {
                    return ValidationProblem();
                }
                var writingCorrectionGroup = _dbContext.WritingCorrectionSubmissionGroups.Add(
                    new WritingCorrectionSubmissionGroup
                    {
                        UserId = userId!,
                        TotalPrice = request.WritingCorrectionPackage.TotalPrice,
                        SubmissionDateTime = DateTime.UtcNow,
                        IsCorrected = false,
                        RequiresEmailResponse = request.WritingCorrectionPackage.RequiresEmailResponse
                    });
                if (writingCorrectionGroup != null)
                {
                    _dbContext.SaveChanges();
                    var service = _dbContext.Services.FirstOrDefault(x => x.Name == "Writing Correction");
                    if (service != null)
                    {
                        var writingCorrectionFiles = new List<WritingCorrectionFile>();
                        foreach (var item in request.WritingCorrectionPackage.ProcessedWritingFiles)
                        {
                            var writingFile = new WritingCorrectionFile
                            {
                                Name = item.WritingFile.Name,
                                Data = item.WritingFile.Data,
                                WordCount = (int)item.WritingFile.WordCount!,
                                Price = item.PriceGroup.Price,
                                PriceName = item.PriceGroup.PriceName,
                                ApplicationWritingTypeId = item.WritingFile.WritingTypeId,
                                WritingCorrectionSubmissionGroup = writingCorrectionGroup.Entity,
                            };
                            writingCorrectionFiles.Add(writingFile);
                            var addedWritingFile = _dbContext.WritingCorrectionFiles.Add(writingFile);
                            writingFile.Id = addedWritingFile.Entity.Id;
                        }
                        var paymentGroup = new PaymentGroup
                        {
                            ServiceId = service.Id,
                            UserId = userId,
                            IsPaymentApproved = false,
                            IsPaymentCheckPending = false,
                            SubmittedServiceId = writingCorrectionGroup.Entity.Id,
                            Message = "No payment files are uploaded"
                        };
                        var addedPaymentGroup = _dbContext.PaymentGroups.Add(paymentGroup);
                        if (addedPaymentGroup is not null)
                        {
                            writingCorrectionGroup.Entity.PaymentGroupId = addedPaymentGroup.Entity.Id;
                            _dbContext.WritingCorrectionSubmissionGroups.Update(writingCorrectionGroup.Entity);
                            var changes = _dbContext.SaveChanges();
                            if (changes > 0)
                            {
                                var user = await _userManager.FindByIdAsync(userId);
                                if (user is not null)
                                {
                                    await _telegramServices.SendMessage
                                        ($"{user.FirstName} {user.LastName} with email address: {user.Email} submitted a writing correction request");
                                }
                                return Ok(new UploadProcessedWritingFilesRequest.Response(
                                    writingCorrectionGroup.Entity.PaymentGroupId));

                            }
                        }
                    }
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
