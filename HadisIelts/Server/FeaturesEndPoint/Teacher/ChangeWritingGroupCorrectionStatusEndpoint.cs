using Ardalis.ApiEndpoints;
using HadisIelts.Server.Data;
using HadisIelts.Server.Models;
using HadisIelts.Server.Services.Email;
using HadisIelts.Shared.Requests.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HadisIelts.Server.FeaturesEndPoint.Teacher
{
    public class ChangeWritingGroupCorrectionStatusEndpoint : EndpointBaseAsync
        .WithRequest<ChangeWritingGroupCorrectionStatusRequest>
        .WithActionResult<ChangeWritingGroupCorrectionStatusRequest.Response>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEmailServices _emailServices;
        private readonly UserManager<ApplicationUser> _userManager;
        public ChangeWritingGroupCorrectionStatusEndpoint(ApplicationDbContext dbContext,
            IEmailServices emailService,
            UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _emailServices = emailService;
            _userManager = userManager;
        }
        [Authorize(Roles = "Administrator,Teacher")]
        [HttpPost(ChangeWritingGroupCorrectionStatusRequest.EndpointUri)]
        public override async Task<ActionResult<ChangeWritingGroupCorrectionStatusRequest.Response>> HandleAsync(ChangeWritingGroupCorrectionStatusRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var writingGroup = await _dbContext.WritingCorrectionSubmissionGroups.FindAsync(request.Id);
                if (writingGroup is not null)
                {
                    var writings = _dbContext.WritingCorrectionFiles.Where
                        (x => x.WritingCorrectionSubmissionGroupId == writingGroup.Id).ToList();
                    var correctedWritings = _dbContext.CorrectedWritingFiles.Where
                        (x => x.WritingCorrectionSubmissionGroupId == writingGroup.Id).ToList();
                    var areAllCorrected = writings.All(x => correctedWritings.Exists(y => y.WritingCorrectionFileId == x.Id));
                    writingGroup.IsCorrected = areAllCorrected;
                    _dbContext.SaveChanges();
                    if (writingGroup.IsCorrected && writingGroup.RequiresEmailResponse)
                    {
                        var user = await _userManager.FindByIdAsync(writingGroup.UserId);
                        if (user is not null)
                        {
                            var emailMessage = new EmailMessage(user.Email!);
                            emailMessage.Subject = "Corrected Writing";
                            emailMessage.Content = "Your submitted writings are corrected and attached to this email. " +
                                "You can download them from this email or from your submitted writings page";
                            emailMessage.AttachmentFiles = correctedWritings;
                            _emailServices.SendEmail(emailMessage);
                        }
                    }
                    return Ok(new ChangeWritingGroupCorrectionStatusRequest.Response(writingGroup.IsCorrected));
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
