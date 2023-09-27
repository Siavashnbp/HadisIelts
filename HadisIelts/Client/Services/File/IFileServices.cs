using HadisIelts.Client.Features.Correction.Models;
using HadisIelts.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace HadisIelts.Client.Services.File
{
    public interface IFileServices
    {
        public Task<string> ReadUploadedFileDataAsync(IBrowserFile file);
        public List<WritingFileSharedModel> ConvertWritingFileModelToWritingFilesAsync(List<WritingFileModel> files);
        public string ViewImageData(string imageData);
    }
}
