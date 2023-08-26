using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Correction;
using HadisIelts.Shared.Requests.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Correction
{
    public class GetSubmittedWritingCorrectionFilesEndpoint : EndpointBaseAsync
        .WithRequest<GetSubmittedWritingCorrectionFilesRequest>
        .WithActionResult<GetSubmittedWritingCorrectionFilesRequest.Response>
    {
        private readonly ICustomRepositoryServices<SubmittedWritingCorrectionFiles, string>
            _submittedWritingCorrectionFilesRepository;
        private readonly ICustomRepositoryServices<WritingCorrectionServicePrice, int>
            _writingCorrectionServicePrice;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        public GetSubmittedWritingCorrectionFilesEndpoint(UserManager<ApplicationUser> userManager
            , ApplicationDbContext dbContext
            , ICustomRepositoryServices<SubmittedWritingCorrectionFiles, string> submittedWritingCorrectionFilesRepository
            , ICustomRepositoryServices<WritingCorrectionServicePrice, int> writingCorrectionServicePrice)
        {
            _submittedWritingCorrectionFilesRepository = submittedWritingCorrectionFilesRepository;
            _userManager = userManager;
            _dbContext = dbContext;
            _writingCorrectionServicePrice = writingCorrectionServicePrice;
        }
        [Authorize]
        [HttpPost(GetSubmittedWritingCorrectionFilesRequest.EndpointUri)]
        public override async Task<ActionResult<GetSubmittedWritingCorrectionFilesRequest.Response>> HandleAsync(GetSubmittedWritingCorrectionFilesRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (request.Request is not null)
                {
                    var user = await _userManager.FindByIdAsync(request.Request.UserID);
                    if (user != null)
                    {
                        var isUserTeacher = await _userManager.IsInRoleAsync(user, "Administrator,Teacher");
                        var submission = await _submittedWritingCorrectionFilesRepository.FindByIDAsync(request.Request.SubmissionID);
                        if (submission != null)
                        {
                            if (!isUserTeacher)
                            {
                                if (submission.UserID != request.Request.UserID)
                                {
                                    return Unauthorized();
                                }
                            }
                            var files = _dbContext.WritingCorrectionFiles.ToList().FindAll
                                (x => x.SubmittedWritingCorecionFilesID == submission.ID);
                            if (files is not null && files.Count > 0)
                            {
                                var writingFiles = new List<ProcessedWritingFile>();
                                foreach (var file in files)
                                {
                                    writingFiles.Add(new ProcessedWritingFile
                                    {
                                        WritingFile = new WritingFile
                                        {
                                            Name = file.Name,
                                            Data = file.Data,
                                            WordCount = file.WordCount,
                                            WritingTypeID = file.ApplicationWritingTypeID
                                        },
                                        PriceGroup = new PriceGroup
                                        {
                                            Price = file.Price,
                                            PriceName = file.PriceName
                                        }
                                    });
                                }
                                return Ok(new GetSubmittedWritingCorrectionFilesRequest.Response(new CalculatedWritingCorrectionPayment
                                {
                                    ProcessedFiles = writingFiles,
                                    TotalPrice = submission.TotalPrice
                                }));
                            }
                            return Ok(new GetSubmittedWritingCorrectionFilesRequest.Response(new CalculatedWritingCorrectionPayment
                            {
                                ProcessedFiles = new List<ProcessedWritingFile>(),
                                TotalPrice = 0,
                                Message = "No files were found"
                            }));
                        }
                        return Ok(new GetSubmittedWritingCorrectionFilesRequest.Response(new CalculatedWritingCorrectionPayment
                        {
                            ProcessedFiles = new List<ProcessedWritingFile>(),
                            TotalPrice = 0,
                            Message = "Submission request was not found"
                        }));
                    }
                    return Unauthorized();
                }
                return BadRequest();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
