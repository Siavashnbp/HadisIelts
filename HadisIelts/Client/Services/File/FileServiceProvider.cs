using HadisIelts.Client.Features.Correction.Models;
using HadisIelts.Shared.Requests.Correction;
using Microsoft.AspNetCore.Components.Forms;

namespace HadisIelts.Client.Services.File
{
    public class FileServiceProvider : IFileServices
    {
        public async Task<List<WritingFile>> ConvertIBrowseFilesToWritingFilesAsync(List<UserWritingFile> files)
        {
            try
            {
                List<WritingFile> writingFiles = new();
                foreach (var file in files)
                {
                    writingFiles.Add(new WritingFile
                    {
                        Data = await ReadUploadedFileDataAsync(file.BrowserFile),
                        Name = file.BrowserFile.Name,
                        WritingTypeID = file.WritingTypeID
                    });
                }
                return writingFiles;
            }
            catch (Exception)
            {
                return null;
            }
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
    }
}
