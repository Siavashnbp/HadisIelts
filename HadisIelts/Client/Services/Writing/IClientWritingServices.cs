using HadisIelts.Shared.Models;

namespace HadisIelts.Client.Services.Writing
{
    public interface IClientWritingServices
    {
        public Task<List<WritingTypeSharedModel>> GetWritingTypesAsync();
    }
}
