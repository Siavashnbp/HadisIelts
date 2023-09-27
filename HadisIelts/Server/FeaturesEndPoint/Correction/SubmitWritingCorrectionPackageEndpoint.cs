using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.User;
using HadisIelts.Shared.Requests.Correction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndpoint.Correction
{
    public class SubmitWritingCorrectionPackageEndpoint : EndpointBaseSync
        .WithRequest<UploadProcessedWritingFilesRequest>
        .WithActionResult<UploadProcessedWritingFilesRequest.Response>
    {
        private readonly IUserServices _userServices;
        private readonly ApplicationDbContext _dbContext;
        public SubmitWritingCorrectionPackageEndpoint(ApplicationDbContext dbContext,
            IUserServices userServices)

        {
            _dbContext = dbContext;
            _userServices = userServices;
        }

        [Authorize]
        [HttpPost(UploadProcessedWritingFilesRequest.EndpointUri)]
        public override ActionResult<UploadProcessedWritingFilesRequest.Response> Handle(UploadProcessedWritingFilesRequest request)
        {
            try
            {
                var userId = _userServices.GetUserIdFromClaims(User.Claims.ToList());
                if (_userServices.HasWritingCorrectionPending(_dbContext, userId))
                {
                    return Problem("You have another writing correction pending");
                }
                var writingCorrectionGroup = _dbContext.WritingCorrectionSubmissionGroups.Add(
                    new WritingCorrectionSubmissionGroup
                    {
                        UserId = userId!,
                        TotalPrice = request.WritingCorrectionPackage.TotalPrice,
                        SubmissionDateTime = DateTime.UtcNow,
                        IsCorrected = false,
                    });
                if (writingCorrectionGroup != null)
                {
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
                                WritingCorrectionSubmissionGroupId = writingCorrectionGroup.Entity.Id,
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
                                return Ok(new UploadProcessedWritingFilesRequest.Response(
                                    writingCorrectionGroup.Entity.PaymentGroupId));

                            }
                        }
                    }
                }
                return Problem("Something went wrong");
            }
            catch (Exception)
            {
                return Problem("Something went wrong");
            }
        }
    }
}
