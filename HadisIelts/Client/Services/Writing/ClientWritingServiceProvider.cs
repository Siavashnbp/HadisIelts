using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Correction;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;

namespace HadisIelts.Client.Services.Writing
{
    public class ClientWritingServiceProvider : IClientWritingServices
    {
        private readonly IMediator _mediator;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public ClientWritingServiceProvider(IMediator mediator
            , AuthenticationStateProvider authenticationStateProvider)
        {
            _mediator = mediator;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<GetSubmittedWritingCorrectionFilesRequest.Response> GetSubmittedWritingCorrectionFiles(string submissionID)
        {
            var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var userID = authenticationState.User.Claims.FirstOrDefault(x => x.Type == "sub").Value;
            if (userID is not null)
            {
                var request = new GetSubmittedWritingCorrectionFilesRequest(
                    UserID: userID,
                    SubmissionID: submissionID
                );
                var response = await _mediator.Send(request);
                return response;
            }
            return new GetSubmittedWritingCorrectionFilesRequest.Response(
            new WritingCorrectionPackageSharedModel
            {
                ProcessedWritingFiles = new List<ProcessedWritingFileSharedModel>(),
                TotalPrice = 0
            }, Message: "User was nor found");
        }

        public async Task<List<WritingTypeSharedModel>> GetWritingTypesAsync()
        {
            var writingTypes = new List<WritingTypeSharedModel>();
            var result = await _mediator.Send(new GetWritingTypesRequest());
            if (result is not null)
            {
                foreach (var item in result.WritingTypes)
                {
                    writingTypes.Add(new WritingTypeSharedModel
                    {
                        ID = item.ID,
                        Name = item.Name,
                    });
                }
            }
            return writingTypes;
        }
    }
}
