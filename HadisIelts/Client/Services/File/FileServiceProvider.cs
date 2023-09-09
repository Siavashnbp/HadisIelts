using HadisIelts.Client.Features.Correction.Models;
using HadisIelts.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace HadisIelts.Client.Services.File
{
    public class FileServiceProvider : IFileServices
    {
        public List<WritingFileSharedModel> ConvertWritingFileModelToWritingFilesAsync(List<WritingFileModel> files)
        {
            List<WritingFileSharedModel> writingFiles = new();
            foreach (var file in files)
            {
                writingFiles.Add(new WritingFileSharedModel
                {
                    Data = file.FileData,
                    Name = file.Name,
                    WritingTypeID = file.WritingType.ID
                });
            }
            return writingFiles;

        }

        public async Task<string> ReadUploadedFileDataAsync(IBrowserFile file)
        {
            using (var ms = new MemoryStream())
            {
                if (file.Size > 0 && file.Size < 10E6)
                {
                    var stream = file.OpenReadStream(maxAllowedSize: (long)10E6);
                    await stream.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    string bytesString = Convert.ToBase64String(fileBytes);
                    return bytesString;
                }
                return null;
            }
        }
        public string ViewImageData(string imageData)
        {
            return String.Format("data:image/gif;base64,{0}", imageData);
        }
    }
}
