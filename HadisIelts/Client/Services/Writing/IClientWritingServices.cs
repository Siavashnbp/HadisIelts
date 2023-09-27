using HadisIelts.Shared.Models;
using HadisIelts.Shared.Requests.Correction;

namespace HadisIelts.Client.Services.Writing
{
    public interface IClientWritingServices
    {
        public Task<List<WritingTypeSharedModel>> GetWritingTypesAsync();
        public Task<GetSubmittedWritingCorrectionFilesRequest.Response> GetSubmittedWritingCorrectionFiles(string submissionId);
    }
}
