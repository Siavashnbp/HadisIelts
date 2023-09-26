using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
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
        private readonly ICustomRepositoryServices<WritingCorrectionSubmissionGroup, string>
            _submittedCorrectionFilesRepository;
        private readonly ICustomRepositoryServices<WritingCorrectionFile, int>
            _writingCorrectionFileRepository;
        private readonly ICustomRepositoryServices<PaymentGroup, string>
            _paymentGroupRepository;
        private readonly IUserServices _userServices;
        private readonly ApplicationDbContext _dbContext;
        public SubmitWritingCorrectionPackageEndpoint(
            ICustomRepositoryServices<WritingCorrectionSubmissionGroup, string> submittedCorrectionFilesRepository,
            ICustomRepositoryServices<WritingCorrectionFile, int> writingCorrectionFileRepository,
            ICustomRepositoryServices<PaymentGroup, string> paymentGroupRepository,
            ApplicationDbContext dbContext,
            IUserServices userServices)

        {
            _submittedCorrectionFilesRepository = submittedCorrectionFilesRepository;
            _writingCorrectionFileRepository = writingCorrectionFileRepository;
            _paymentGroupRepository = paymentGroupRepository;
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
                var writingCorrectionGroup = _submittedCorrectionFilesRepository.Insert(
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
                                WritingCorrectionSubmissionGroup = writingCorrectionGroup,
                                WritingCorrectionSubmissionGroupId = writingCorrectionGroup.Id,
                            };
                            writingCorrectionFiles.Add(writingFile);
                            var addedWritingFile = _writingCorrectionFileRepository.Insert(writingFile);
                            writingFile.Id = addedWritingFile.Id;
                        }
                        var paymentGroup = new PaymentGroup
                        {
                            ServiceId = service.Id,
                            UserId = userId,
                            IsPaymentApproved = false,
                            IsPaymentCheckPending = false,
                            SubmittedServiceId = writingCorrectionGroup.Id,
                            Message = "No payment files are uploaded"
                        };
                        var addedPaymentGroup = _paymentGroupRepository.Insert(paymentGroup);
                        if (addedPaymentGroup is not null)
                        {
                            writingCorrectionGroup.PaymentGroupId = addedPaymentGroup.Id;
                            _submittedCorrectionFilesRepository.Update(writingCorrectionGroup);
                            return Ok(new UploadProcessedWritingFilesRequest.Response(writingCorrectionGroup.PaymentGroupId));
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
