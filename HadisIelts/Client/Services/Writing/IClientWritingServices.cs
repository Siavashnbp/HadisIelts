using HadisIelts.Client.Features.Teacher.Models;
using HadisIelts.Shared.Requests.Payment;

namespace HadisIelts.Client.Services.Writing
{
    public interface IClientWritingServices
    {
        public Task<List<WritingTypeModel>> GetWritingTypesAsync();
        public Task<CalculatedWritingCorrectionPayment> GetSubmittedWritingCorrectionFiles(string submissionID);
    }
}
