using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Requests.Correction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HadisIelts.Server.FeaturesEndpoint.Correction
{
    public class SubmitWritingCorrectionPackageEndpoint : EndpointBaseAsync
        .WithRequest<UploadProcessedWritingFilesRequest>
        .WithActionResult<UploadProcessedWritingFilesRequest.Response>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICustomRepositoryServices<WritingCorrectionSubmissionGroup, string>
            _submittedCorrectionFilesRepository;
        private readonly ICustomRepositoryServices<WritingCorrectionFile, int>
            _writingCorrectionFileRepository;
        private readonly ICustomRepositoryServices<PaymentGroup, string>
            _paymentGroupRepository;
        private readonly ApplicationDbContext _applicationDbContext;
        public SubmitWritingCorrectionPackageEndpoint(UserManager<ApplicationUser> userManager
            , ICustomRepositoryServices<WritingCorrectionSubmissionGroup, string> submittedCorrectionFilesRepository
            , ICustomRepositoryServices<WritingCorrectionFile, int> writingCorrectionFileRepository
            , ICustomRepositoryServices<PaymentGroup, string> paymentGroupRepository
            , ApplicationDbContext applicationDbContext)

        {
            _userManager = userManager;
            _submittedCorrectionFilesRepository = submittedCorrectionFilesRepository;
            _writingCorrectionFileRepository = writingCorrectionFileRepository;
            _paymentGroupRepository = paymentGroupRepository;
            _applicationDbContext = applicationDbContext;
        }
        [Authorize]
        [HttpPost(UploadProcessedWritingFilesRequest.EndpointUri)]
        public override async Task<ActionResult<UploadProcessedWritingFilesRequest.Response>> HandleAsync(UploadProcessedWritingFilesRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var userID = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)!.Value;
                var writingCorrectionGroup = _submittedCorrectionFilesRepository.Insert(
                    new WritingCorrectionSubmissionGroup
                    {
                        UserID = userID!,
                        TotalPrice = request.WritingCorrectionPackage.TotalPrice,
                        SubmissionDateTime = DateTime.UtcNow
                    });
                if (writingCorrectionGroup != null)
                {
                    var service = _applicationDbContext.Services.FirstOrDefault(x => x.Name == "Writing Correction");
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
                                ApplicationWritingTypeID = item.WritingFile.WritingTypeID,
                                SubmittedWritingCorrectionFiles = writingCorrectionGroup,
                                SubmittedWritingCorecionFilesID = writingCorrectionGroup.ID,
                            };
                            writingCorrectionFiles.Add(writingFile);
                            var addedWritingFile = _writingCorrectionFileRepository.Insert(writingFile);
                            writingFile.ID = addedWritingFile.ID;
                        }
                        var paymentGroup = new PaymentGroup
                        {
                            ServiceID = service.ID,
                            UserID = userID,
                            IsPaymentApproved = false,
                            IsPaymentCheckPending = false,
                            SubmittedServiceID = writingCorrectionGroup.ID,
                            Message = "No payment files are uploaded"
                        };
                        var addedPaymentGroup = _paymentGroupRepository.Insert(paymentGroup);
                        if (addedPaymentGroup is not null)
                        {
                            writingCorrectionGroup.PaymentGroupID = addedPaymentGroup.ID;
                            _submittedCorrectionFilesRepository.Update(writingCorrectionGroup);
                            return Ok(new UploadProcessedWritingFilesRequest.Response(writingCorrectionGroup.PaymentGroupID));
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
