using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models;
using HadisIelts.Server.Models.Entities;
using HadisIelts.Server.Services.DbServices;
using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Correction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Correction
{
    public class GetSubmittedWritingCorrectionFilesEndpoint : EndpointBaseAsync
        .WithRequest<GetSubmittedWritingCorrectionFilesRequest>
        .WithActionResult<GetSubmittedWritingCorrectionFilesRequest.Response>
    {
        private readonly ICustomRepositoryServices<WritingCorrectionSubmissionGroup, string>
            _submittedWritingCorrectionFilesRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        public GetSubmittedWritingCorrectionFilesEndpoint(UserManager<ApplicationUser> userManager
            , ApplicationDbContext dbContext
            , ICustomRepositoryServices<WritingCorrectionSubmissionGroup, string> submittedWritingCorrectionFilesRepository)
        {
            _submittedWritingCorrectionFilesRepository = submittedWritingCorrectionFilesRepository;
            _userManager = userManager;
            _dbContext = dbContext;
        }
        /// <summary>
        /// gets submitted writing correction files of a user
        /// </summary>
        /// <param name="request">
        /// UserID: ID of issuer user
        /// SubmissionID: ID of submitted group
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns>
        /// Unauthorized if found user is different and is not in admin or teacher role
        /// WritingCorrectionPackage, with appropriate message
        /// </returns>
        [Authorize]
        [HttpPost(GetSubmittedWritingCorrectionFilesRequest.EndpointUri)]
        public override async Task<ActionResult<GetSubmittedWritingCorrectionFilesRequest.Response>> HandleAsync(GetSubmittedWritingCorrectionFilesRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserID);
                if (user != null)
                {
                    var isUserTeacher = await _userManager.IsInRoleAsync(user, "Administrator,Teacher");
                    var submission = await _submittedWritingCorrectionFilesRepository.FindByIDAsync(request.SubmissionID);
                    if (submission != null)
                    {
                        if (!isUserTeacher)
                        {
                            if (submission.UserID != request.UserID)
                            {
                                return Unauthorized();
                            }
                        }
                        var files = _dbContext.WritingCorrectionFiles.ToList().FindAll
                            (x => x.WritingCorrectionSubmissionGroupID == submission.ID);
                        var correctedFiles = _dbContext.CorrectedWritingFiles.ToList().FindAll
                            (x => x.WritingCorrectionSubmissionGroupID == submission.ID);
                        if (files is not null && files.Count > 0)
                        {
                            var writingFiles = new List<ProcessedWritingFileSharedModel>();
                            foreach (var file in files)
                            {
                                var correctedFile = correctedFiles.FirstOrDefault(x => x.WritingCorrectionFileID == file.ID);
                                writingFiles.Add(new ProcessedWritingFileSharedModel
                                {
                                    WritingFile = new WritingFileSharedModel
                                    {
                                        Name = file.Name,
                                        Data = file.Data,
                                        WordCount = file.WordCount,
                                        WritingTypeID = file.ApplicationWritingTypeID
                                    },
                                    PriceGroup = new PriceGroupSharedModel
                                    {
                                        Price = file.Price,
                                        PriceName = file.PriceName
                                    },
                                    CorrectedWriting = correctedFile is not null ? new CorrectedWritingSharedModel
                                    {
                                        ID = correctedFile.ID,
                                        Data = correctedFile.Data,
                                        Name = correctedFile.Name,
                                        UploadDateTime = correctedFile.UploadDateTime,
                                        WritingFileID = correctedFile.WritingCorrectionFileID
                                    } : null
                                });
                            }
                            return Ok(new GetSubmittedWritingCorrectionFilesRequest.Response(
                                new WritingCorrectionPackageSharedModel
                                {
                                    ProcessedWritingFiles = writingFiles,
                                    TotalPrice = submission.TotalPrice,
                                }
                                , Message: string.Empty));
                        }
                        return Ok(new GetSubmittedWritingCorrectionFilesRequest.Response(
                            WritingCorrectionPackage: new WritingCorrectionPackageSharedModel
                            {
                                ProcessedWritingFiles = new List<ProcessedWritingFileSharedModel>(),
                                TotalPrice = 0,
                            },
                            Message: "No files were found"));

                    }
                    return Ok(new GetSubmittedWritingCorrectionFilesRequest.Response(
                        new WritingCorrectionPackageSharedModel
                        {
                            ProcessedWritingFiles = new List<ProcessedWritingFileSharedModel>(),
                            TotalPrice = 0,
                        }
                    , Message: "Submission request was not found"));
                }
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
