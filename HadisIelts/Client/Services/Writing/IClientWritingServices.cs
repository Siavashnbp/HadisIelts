using HadisIelts.Client.Features.Teacher.Models;

namespace HadisIelts.Client.Services.Writing
{
    public interface IClientWritingServices
    {
        public Task<List<WritingTypeModel>> GetWritingTypesAsync();
    }
}
